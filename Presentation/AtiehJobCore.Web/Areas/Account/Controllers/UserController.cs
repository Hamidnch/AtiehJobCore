using AtiehJobCore.Core.Constants;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain;
using AtiehJobCore.Core.Domain.Employers;
using AtiehJobCore.Core.Domain.Jobseekers;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Services.Authentication;
using AtiehJobCore.Services.Common;
using AtiehJobCore.Services.Employers;
using AtiehJobCore.Services.Events;
using AtiehJobCore.Services.Jobseekers;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Services.Logging;
using AtiehJobCore.Services.Users;
using AtiehJobCore.Web.Framework.Filters;
using AtiehJobCore.Web.Framework.Models.Account;
using AtiehJobCore.Web.Framework.Models.Account.Employer;
using AtiehJobCore.Web.Framework.Models.Account.Jobseeker;
using AtiehJobCore.Web.Framework.Mvc.Captcha;
using AtiehJobCore.Web.Framework.Security;
using AtiehJobCore.Web.Framework.Services;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace AtiehJobCore.Web.Areas.Account.Controllers
{
    [Area(AreaNames.Account)]
    [BreadCrumb(Title = "حساب کاربری", UseDefaultRouteUrl = true, Order = 0)]
    public class UserController : Controller
    {
        #region Fields
        private readonly IUserViewModelService _userViewModelService;
        private readonly ILocalizationService _localizationService;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IUserService _userService;
        private readonly IAtiehJobAuthenticationService _atiehJobAuthenticationService;
        private readonly IUserActivityService _userActivityService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IUserAttributeParser _userAttributeParser;
        private readonly IEventPublisher _eventPublisher;
        private readonly IWorkContext _workContext;
        private readonly IJobseekerService _jobseekerService;
        private readonly IEmployerService _employerService;
        private readonly CaptchaSettings _captchaSettings;
        private readonly UserSettings _userSettings;
        #endregion Fields

        #region Ctor
        public UserController(IUserViewModelService userViewModelService,
            CaptchaSettings captchaSettings, ILocalizationService localizationService,
            UserSettings userSettings, IUserRegistrationService userRegistrationService,
            IUserService userService, IAtiehJobAuthenticationService atiehJobAuthenticationService,
            IEventPublisher eventPublisher, IUserActivityService userActivityService,
            IWorkContext workContext, IUserAttributeParser userAttributeParser,
            IGenericAttributeService genericAttributeService, IJobseekerService jobseekerService,
            IEmployerService employerService)
        {
            _userViewModelService = userViewModelService;
            _captchaSettings = captchaSettings;
            _localizationService = localizationService;
            _userSettings = userSettings;
            _userRegistrationService = userRegistrationService;
            _userService = userService;
            _atiehJobAuthenticationService = atiehJobAuthenticationService;
            _eventPublisher = eventPublisher;
            _userActivityService = userActivityService;
            _workContext = workContext;
            _userAttributeParser = userAttributeParser;
            _genericAttributeService = genericAttributeService;
            _jobseekerService = jobseekerService;
            _employerService = employerService;
        }
        #endregion Ctor

        #region Login / logout

        [CheckAccessSite(true)]
        [CheckAccessClosedSite(true)]
        public virtual IActionResult Login()
        {
            var model = _userViewModelService.PrepareLoginModel();
            //if (Request.IsAjaxRequest())
            //{
            //return PartialView("_Login", model);
            //}

            return View(model);
        }

        [HttpPost]
        [ValidateCaptcha]
        [PublicAntiForgery]
        [CheckAccessSite(true)]
        [CheckAccessClosedSite(true)]
        public virtual IActionResult Login(LoginModel model, string returnUrl, bool captchaValid)
        {
            //validate CAPTCHA
            if (_captchaSettings.Enabled && _captchaSettings.ShowOnLoginPage && !captchaValid)
            {
                ModelState.AddModelError("",
                    _captchaSettings.GetWrongCaptchaMessage(_localizationService));
            }

            if (ModelState.IsValid)
            {
                if (_userSettings.UsernamesEnabled && model.Username != null)
                {
                    model.Username = model.Username.Trim();
                }
                else
                {
                    model.EmailOrMobileOrNationalCode = model.EmailOrMobileOrNationalCode.Trim();
                }

                var loginResult = _userRegistrationService.ValidateUser(_userSettings.UsernamesEnabled
                    ? model.Username : model.EmailOrMobileOrNationalCode, model.Password);
                switch (loginResult)
                {
                    case UserLoginResults.Successful:
                        {
                            var user = _userSettings.UsernamesEnabled
                                ? _userService.GetUserByUsername(model.Username)
                                : _userService.GetUserByEmail(model.EmailOrMobileOrNationalCode)
                               ?? _userService.GetUserByMobileNumber(model.EmailOrMobileOrNationalCode)
                               ?? _userService.GetUserByNationalCode(model.EmailOrMobileOrNationalCode);

                            //sign in new user
                            _atiehJobAuthenticationService.SignIn(user, model.RememberMe);

                            //raise event       
                            _eventPublisher.Publish(new UserLoggedInEvent(user));

                            //activity log
                            _userActivityService.InsertActivity("AtiehJob.Login", "",
                                _localizationService.GetResource("ActivityLog.PublicStore.Login"), user);


                            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                                return RedirectToRoute("HomePage");

                            return Redirect(returnUrl);
                        }
                    case UserLoginResults.UserNotExist:
                        ModelState.AddModelError("",
                            _localizationService.GetResource("Account.Login.WrongCredentials.UserNotExist"));
                        break;
                    case UserLoginResults.Deleted:
                        ModelState.AddModelError("",
                            _localizationService.GetResource("Account.Login.WrongCredentials.Deleted"));
                        break;
                    case UserLoginResults.NotActive:
                        ModelState.AddModelError("",
                            _localizationService.GetResource("Account.Login.WrongCredentials.NotActive"));
                        break;
                    case UserLoginResults.NotRegistered:
                        ModelState.AddModelError("",
                            _localizationService.GetResource("Account.Login.WrongCredentials.NotRegistered"));
                        break;
                    case UserLoginResults.LockedOut:
                        ModelState.AddModelError("",
                            _localizationService.GetResource("Account.Login.WrongCredentials.LockedOut"));
                        break;
                    case UserLoginResults.WrongPassword:
                        ModelState.AddModelError("",
                            _localizationService.GetResource("Account.Login.WrongCredentials"));
                        break;
                    default:
                        ModelState.AddModelError("",
                            _localizationService.GetResource("Account.Login.WrongCredentials"));
                        break;
                }
            }

            //If we got this far, something failed, redisplay form
            //model.UserLoginType = _userSettings.UserLoginType;
            model.DisplayCaptcha = _captchaSettings.Enabled && _captchaSettings.ShowOnLoginPage;
            return View(model);
        }

        [CheckAccessSite(true)]
        [CheckAccessClosedSite(true)]
        public virtual IActionResult Logout([FromServices] StoreInformationSettings storeInformationSettings)
        {
            //activity log
            _userActivityService.InsertActivity("AtiehJob.Logout", "",
                _localizationService.GetResource("ActivityLog.AtiehJob.Logout"));
            //standard logout 
            _atiehJobAuthenticationService.SignOut();

            //EU Cookie
            if (storeInformationSettings.DisplayEuCookieLawWarning)
            {
                //the cookie law message should not pop up immediately after logout.
                //otherwise, the user will have to click it again...
                //and thus next visitor will not click it... so violation for that cookie law..
                //the only good solution in this case is to store a temporary variable
                //indicating that the EU cookie popup window should not be displayed on the next page open (after logout redirection to homepage)
                //but it'll be displayed for further page loads
                TempData["AtiehJob.IgnoreEuCookieLawWarning"] = true;
            }
            return RedirectToRoute("HomePage");
        }

        #endregion

        #region Register
        //available even when navigation is not allowed
        [CheckAccessSite(true)]
        public virtual IActionResult RegisterJobseeker()
        {
            //check whether registration is allowed
            if (_userSettings.UserRegistrationType == UserRegistrationType.Disabled)
                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.Disabled });

            var model = new RegisterJobseekerModel();
            model = _userViewModelService.PrepareRegisterJobseekerModel(model, false);

            return View(model);
        }

        [HttpPost]
        [ValidateCaptcha]
        [ValidateHoneypot]
        [PublicAntiForgery]
        //available even when navigation is not allowed
        [CheckAccessSite(true)]
        public virtual IActionResult RegisterJobseeker(RegisterJobseekerModel model, string returnUrl, bool captchaValid, IFormCollection form)
        {
            //check whether registration is allowed
            if (_userSettings.UserRegistrationType == UserRegistrationType.Disabled)
                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.Disabled });

            if (_workContext.CurrentUser.IsRegistered())
            {
                //Already registered user. 
                _atiehJobAuthenticationService.SignOut();

                //Save a new record
                _workContext.CurrentUser = _userService.InsertGuestUser();
            }
            var user = _workContext.CurrentUser;

            //custom user attributes
            var userAttributesXml = _userViewModelService.ParseCustomAttributes(form);
            var userAttributeWarnings = _userAttributeParser.GetAttributeWarnings(userAttributesXml);
            foreach (var error in userAttributeWarnings)
            {
                ModelState.AddModelError("", error);
            }

            //validate CAPTCHA
            if (_captchaSettings.Enabled && _captchaSettings.ShowOnRegistrationPage && !captchaValid)
            {
                ModelState.AddModelError("", _captchaSettings.GetWrongCaptchaMessage(_localizationService));
            }

            if (ModelState.IsValid && ModelState.ErrorCount == 0)
            {
                if (_userSettings.UsernamesEnabled && model.Username != null)
                {
                    model.Username = model.Username.Trim();
                }

                var isApproved = _userSettings.UserRegistrationType == UserRegistrationType.Standard;

                var registrationRequest = new UserRegistrationRequest(user, model.Email,
                    _userSettings.UsernamesEnabled ? model.Username : model.Email,
                    model.MobileNumber, model.NationalCode, model.Password,
                    _userSettings.DefaultPasswordFormat, UserType.Jobseeker, isApproved);

                var registrationResult = _userRegistrationService.RegisterUser(registrationRequest);

                if (registrationResult.Success)
                {
                    var jobseeker = new Jobseeker
                    {
                        //NationalCode = model.NationalCode,
                        //MobileNumber = model.MobileNumber,
                        FileNumber = user.UserGuid.ToString().Replace("-", ""),
                        UserId = user.Id,
                        EnrollDate = DateTime.UtcNow.Date,
                        EnrollTime = $"{DateTime.UtcNow:HH:mm}",
                        //Email = model.Email,
                        Name = model.FirstName,
                        Family = model.LastName,
                        CurrentState = JobseekerState.PrimaryRegisteration,
                        User = user
                    };

                    var newJobseeker = _jobseekerService.InsertJobseeker(jobseeker);

                    user.Jobseekers.Add(newJobseeker);
                    _userService.UpdateUser(user);

                    //form fields
                    if (_userSettings.GenderEnabled)
                        _genericAttributeService.SaveAttribute(user, SystemUserAttributeNames.Gender, model.Gender);
                    _genericAttributeService.SaveAttribute(user, SystemUserAttributeNames.FirstName, model.FirstName);
                    _genericAttributeService.SaveAttribute(user, SystemUserAttributeNames.LastName, model.LastName);

                    //save user attributes
                    _genericAttributeService.SaveAttribute(user, SystemUserAttributeNames.CustomUserAttributes, userAttributesXml);

                    //login user now
                    if (isApproved)
                        _atiehJobAuthenticationService.SignIn(user, true);


                    //notifications
                    //if (_userSettings.NotifyNewUserRegistration)
                    //    _workflowMessageService.SendUserRegisteredNotificationMessage(user, _localizationSettings.DefaultAdminLanguageId);

                    //raise event       
                    _eventPublisher.Publish(new UserRegisteredEvent(user));

                    switch (_userSettings.UserRegistrationType)
                    {
                        case UserRegistrationType.EmailValidation:
                            {
                                //email validation message
                                _genericAttributeService.SaveAttribute(user, SystemUserAttributeNames.AccountActivationToken,
                                    Guid.NewGuid().ToString());
                                //_workflowMessageService.SendUserEmailValidationMessage(user, _workContext.WorkingLanguage.Id);

                                //result
                                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.EmailValidation });
                            }
                        case UserRegistrationType.AdminApproval:
                            {
                                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.AdminApproval });
                            }
                        case UserRegistrationType.Standard:
                            {
                                //send user welcome message
                                //_workflowMessageService.SendUserWelcomeMessage(user, _workContext.WorkingLanguage.Id);

                                var redirectUrl = Url.RouteUrl("RegisterResult", new { resultId = (int)UserRegistrationType.Standard });
                                if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                                {
                                    return Redirect(redirectUrl);
                                }

                                var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                                redirectUrl = webHelper.ModifyQueryString(redirectUrl,
                                    "returnurl=" + WebUtility.UrlEncode(returnUrl), null);
                                return Redirect(redirectUrl);
                            }
                        case UserRegistrationType.MobileValidation:
                            break;
                        case UserRegistrationType.Disabled:
                            break;
                        default:
                            {
                                return RedirectToRoute("HomePage");
                            }
                    }
                }

                //errors
                foreach (var error in registrationResult.Errors)
                    ModelState.AddModelError("", error);
            }

            //If we got this far, something failed, redisplay form
            model = _userViewModelService.PrepareRegisterJobseekerModel(model, true, userAttributesXml);
            return View(model);
        }

        [CheckAccessSite(true)]
        public virtual IActionResult RegisterEmployer()
        {
            //check whether registration is allowed
            if (_userSettings.UserRegistrationType == UserRegistrationType.Disabled)
                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.Disabled });

            var model = new RegisterEmployerModel();
            model = _userViewModelService.PrepareRegisterEmployerModel(model, false);

            return View(model);
        }

        [HttpPost]
        [ValidateCaptcha]
        [ValidateHoneypot]
        [PublicAntiForgery]
        //available even when navigation is not allowed
        [CheckAccessSite(true)]
        public virtual IActionResult RegisterEmployer(RegisterEmployerModel model, string returnUrl, bool captchaValid, IFormCollection form)
        {
            //check whether registration is allowed
            if (_userSettings.UserRegistrationType == UserRegistrationType.Disabled)
                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.Disabled });

            if (_workContext.CurrentUser.IsRegistered())
            {
                //Already registered user. 
                _atiehJobAuthenticationService.SignOut();

                //Save a new record
                _workContext.CurrentUser = _userService.InsertGuestUser();
            }
            var user = _workContext.CurrentUser;

            //custom user attributes
            var userAttributesXml = _userViewModelService.ParseCustomAttributes(form);
            var userAttributeWarnings = _userAttributeParser.GetAttributeWarnings(userAttributesXml);
            foreach (var error in userAttributeWarnings)
            {
                ModelState.AddModelError("", error);
            }

            //validate CAPTCHA
            if (_captchaSettings.Enabled && _captchaSettings.ShowOnRegistrationPage && !captchaValid)
            {
                ModelState.AddModelError("", _captchaSettings.GetWrongCaptchaMessage(_localizationService));
            }

            if (ModelState.IsValid && ModelState.ErrorCount == 0)
            {
                if (_userSettings.UsernamesEnabled && model.Username != null)
                {
                    model.Username = model.Username.Trim();
                }

                var isApproved = _userSettings.UserRegistrationType == UserRegistrationType.Standard;

                var registrationRequest = new UserRegistrationRequest(user, model.Email,
                    _userSettings.UsernamesEnabled ? model.Username : model.Email,
                    model.MobileNumber, model.NationalCode, model.Password,
                    _userSettings.DefaultPasswordFormat, UserType.Employer, isApproved);

                var registrationResult = _userRegistrationService.RegisterUser(registrationRequest);

                if (registrationResult.Success)
                {
                    var employer = new Employer
                    {
                        //NationalCode = model.NationalCode,
                        //MobileNumber = model.MobileNumber,
                        FileNumber = user.UserGuid.ToString().Replace("-", ""),
                        UserId = user.Id,
                        EnrollDate = DateTime.UtcNow.Date,
                        EnrollTime = $"{DateTime.UtcNow:HH:mm}",
                        //Email = model.Email,
                        InsuranceCode = model.InsuranceCode,
                        CompanyName = model.CompanyName,
                        ManagerName = model.ManagerName,
                        CurrentState = EmployerState.PrimaryRegisteration,
                        User = user
                    };

                    var newEmployer = _employerService.InsertEmployer(employer);

                    user.Employers.Add(newEmployer);
                    _userService.UpdateUser(user);

                    //save user attributes
                    _genericAttributeService.SaveAttribute(user, SystemUserAttributeNames.CustomUserAttributes, userAttributesXml);

                    //login user now
                    if (isApproved)
                        _atiehJobAuthenticationService.SignIn(user, true);


                    //notifications
                    //if (_userSettings.NotifyNewUserRegistration)
                    //    _workflowMessageService.SendUserRegisteredNotificationMessage(user, _localizationSettings.DefaultAdminLanguageId);

                    //raise event       
                    _eventPublisher.Publish(new UserRegisteredEvent(user));

                    switch (_userSettings.UserRegistrationType)
                    {
                        case UserRegistrationType.EmailValidation:
                            {
                                //email validation message
                                _genericAttributeService.SaveAttribute(user, SystemUserAttributeNames.AccountActivationToken,
                                    Guid.NewGuid().ToString());
                                //_workflowMessageService.SendUserEmailValidationMessage(user, _workContext.WorkingLanguage.Id);

                                //result
                                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.EmailValidation });
                            }
                        case UserRegistrationType.AdminApproval:
                            {
                                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.AdminApproval });
                            }
                        case UserRegistrationType.Standard:
                            {
                                //send user welcome message
                                //_workflowMessageService.SendUserWelcomeMessage(user, _workContext.WorkingLanguage.Id);

                                var redirectUrl = Url.RouteUrl("RegisterResult", new { resultId = (int)UserRegistrationType.Standard });
                                if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                                {
                                    return Redirect(redirectUrl);
                                }

                                var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                                redirectUrl = webHelper.ModifyQueryString(redirectUrl,
                                    "returnurl=" + WebUtility.UrlEncode(returnUrl), null);
                                return Redirect(redirectUrl);
                            }
                        case UserRegistrationType.MobileValidation:
                            {
                                //result
                                return RedirectToRoute("RegisterResult",
                                    new { resultId = (int)UserRegistrationType.MobileValidation });
                            }

                        case UserRegistrationType.Disabled:
                            break;
                        default:
                            {
                                return RedirectToRoute("HomePage");
                            }
                    }
                }

                //errors
                foreach (var error in registrationResult.Errors)
                    ModelState.AddModelError("", error);
            }

            //If we got this far, something failed, redisplay form
            model = _userViewModelService.PrepareRegisterEmployerModel(model, true, userAttributesXml);
            return View(model);
        }

        //available even when navigation is not allowed
        [CheckAccessSite(true)]
        public virtual IActionResult RegisterResult(int resultId)
        {
            var resultText = "";
            switch ((UserRegistrationType)resultId)
            {
                case UserRegistrationType.Disabled:
                    resultText = _localizationService.GetResource("Account.Register.Result.Disabled");
                    break;
                case UserRegistrationType.Standard:
                    resultText = _localizationService.GetResource("Account.Register.Result.Standard");
                    break;
                case UserRegistrationType.AdminApproval:
                    resultText = _localizationService.GetResource("Account.Register.Result.AdminApproval");
                    break;
                case UserRegistrationType.EmailValidation:
                    resultText = _localizationService.GetResource("Account.Register.Result.EmailValidation");
                    break;
                case UserRegistrationType.MobileValidation:
                    resultText = _localizationService.GetResource("Account.Register.Result.MobileValidation");
                    break;
                default:
                    break;
            }
            var model = new RegisterResultModel
            {
                Result = resultText
            };
            return View(model);
        }
        #endregion Register
    }
}
