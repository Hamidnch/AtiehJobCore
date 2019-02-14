using AtiehJobCore.Core.Constants;
using AtiehJobCore.Core.Domain;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Services.Authentication;
using AtiehJobCore.Services.Events;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Services.Logging;
using AtiehJobCore.Services.Users;
using AtiehJobCore.Web.Framework.Filters;
using AtiehJobCore.Web.Framework.Models.Account;
using AtiehJobCore.Web.Framework.Mvc.Captcha;
using AtiehJobCore.Web.Framework.Security;
using AtiehJobCore.Web.Framework.Services;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Mvc;

namespace AtiehJobCore.Web.Areas.Account.Controllers
{
    [Area(AreaNames.Account)]
    [BreadCrumb(Title = "حساب کاربری", UseDefaultRouteUrl = true, Order = 0)]
    public class UserController : Controller
    {
        private readonly IUserViewModelService _userViewModelService;
        private readonly ILocalizationService _localizationService;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IUserService _userService;
        private readonly IAtiehJobAuthenticationService _atiehJobAuthenticationService;
        private readonly IUserActivityService _userActivityService;
        private readonly IEventPublisher _eventPublisher;
        private readonly CaptchaSettings _captchaSettings;
        private readonly UserSettings _userSettings;
        public UserController(IUserViewModelService userViewModelService,
            CaptchaSettings captchaSettings, ILocalizationService localizationService,
            UserSettings userSettings, IUserRegistrationService userRegistrationService,
            IUserService userService, IAtiehJobAuthenticationService atiehJobAuthenticationService,
            IEventPublisher eventPublisher, IUserActivityService userActivityService)
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
        }

        #region Login / logout

        [CheckAccessSite(true)]
        [CheckAccessClosedSite(true)]
        public virtual IActionResult Login()
        {
            var model = _userViewModelService.PrepareLogin();
            return View(model);
        }

        [HttpPost]
        [ValidateCaptcha]
        [PublicAntiForgery]
        [CheckAccessSite(true)]
        [CheckAccessClosedSite(true)]
        public virtual IActionResult Login(LoginModel model, string returnUrl, bool captchaValid, string man)
        {
            //validate CAPTCHA
            if (_captchaSettings.Enabled && _captchaSettings.ShowOnLoginPage && !captchaValid)
            {
                ModelState.AddModelError("",
                    _captchaSettings.GetWrongCaptchaMessage(_localizationService));
            }

            if (ModelState.IsValid)
            {
                //if (_userSettings.UserLoginType == UserLoginType.Username && model.Username != null)
                //{
                //    model.Username = model.Username.Trim();
                //}

                string userInput;
                var userLoginType = _userSettings.UserLoginType;
                switch (userLoginType)
                {
                    case UserLoginType.Username:
                        userInput = model.Username.Trim();
                        break;
                    case UserLoginType.Email:
                        userInput = model.Email.Trim();
                        break;
                    case UserLoginType.MobileNumber:
                        userInput = model.MobileNumber.Trim();
                        break;
                    case UserLoginType.NationalCode:
                        userInput = model.NationalCode.Trim();
                        break;
                    default:
                        userInput = model.Email.Trim();
                        break;
                }

                var loginResult = _userRegistrationService.ValidateUser(userInput, model.Password);
                switch (loginResult)
                {
                    case UserLoginResults.Successful:
                        {
                            User user;

                            switch (userLoginType)
                            {
                                case UserLoginType.Username:
                                    user = _userService.GetUserByUsername(model.Username);
                                    break;
                                case UserLoginType.Email:
                                    user = _userService.GetUserByEmail(model.Email);
                                    break;
                                case UserLoginType.MobileNumber:
                                    user = _userService.GetUserByMobileNumber(model.MobileNumber);
                                    break;
                                case UserLoginType.NationalCode:
                                    user = _userService.GetUserByNationalCode(model.NationalCode);
                                    break;
                                default:
                                    user = _userService.GetUserByEmail(model.Email);
                                    break;
                            }

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
                _localizationService.GetResource("ActivityLog.PublicStore.Logout"));
            //standard logout 
            _atiehJobAuthenticationService.SignOut();

            ////EU Cookie
            //if (storeInformationSettings.DisplayEuCookieLawWarning)
            //{
            //    //the cookie law message should not pop up immediately after logout.
            //    //otherwise, the user will have to click it again...
            //    //and thus next visitor will not click it... so violation for that cookie law..
            //    //the only good solution in this case is to store a temporary variable
            //    //indicating that the EU cookie popup window should not be displayed on the next page open (after logout redirection to homepage)
            //    //but it'll be displayed for further page loads
            //    TempData["AtiehJob.IgnoreEuCookieLawWarning"] = true;
            //}
            return RedirectToRoute("HomePage");
        }

        #endregion
    }
}
