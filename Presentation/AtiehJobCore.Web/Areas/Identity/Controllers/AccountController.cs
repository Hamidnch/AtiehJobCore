using System;
using System.Threading.Tasks;
using AtiehJobCore.Common.Constants;
using AtiehJobCore.Common.Extensions;
using AtiehJobCore.Domain.Entities.Identity;
using AtiehJobCore.Services.Identity.Interfaces;
using AtiehJobCore.ViewModel.Models.Identity.Account;
using AtiehJobCore.ViewModel.Models.Identity.Emails;
using AtiehJobCore.ViewModel.Models.Identity.Password;
using AtiehJobCore.ViewModel.Models.Identity.Settings;
using AtiehJobCore.Web.Controllers;
using DNTBreadCrumb.Core;
using DNTCaptcha.Core;
using DNTCommon.Web.Core;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AtiehJobCore.Web.Areas.Identity.Controllers
{
    [Area(AreaNames.Identity)]
    [BreadCrumb(Title = "حساب کاربری", UseDefaultRouteUrl = true, Order = 0)]
    public class AccountController : Controller
    {
        #region Fields
        private readonly ILogger<AccountController> _logger;
        private readonly ISignInManager _signInManager;
        private readonly IUserManager _userManager;
        private readonly IOptionsSnapshot<SiteSettings> _siteSettings;
        private readonly IEmailSender _emailSender;
        private readonly IUserValidator<User> _userValidator;
        private readonly IPasswordValidator<User> _passwordValidator;
        private readonly IUsedPasswordsService _usedPasswordsService;
        #endregion Fields

        #region Ctor
        public AccountController(ILogger<AccountController> logger, ISignInManager signInManager,
            IUserManager userManager, IOptionsSnapshot<SiteSettings> siteSettings,
            IEmailSender emailSender, IUserValidator<User> userValidator,
            IPasswordValidator<User> passwordValidator, IUsedPasswordsService usedPasswordsService)
        {
            _logger = logger;
            _logger.CheckArgumentIsNull(nameof(_logger));

            _signInManager = signInManager;
            _signInManager.CheckArgumentIsNull(nameof(_signInManager));

            _userManager = userManager;
            _userManager.CheckArgumentIsNull(nameof(_userManager));

            _siteSettings = siteSettings;
            _siteSettings.CheckArgumentIsNull(nameof(_siteSettings));

            _emailSender = emailSender;
            _emailSender.CheckArgumentIsNull(nameof(_emailSender));

            _userValidator = userValidator;
            _userValidator.CheckArgumentIsNull(nameof(_userValidator));

            _passwordValidator = passwordValidator;
            _passwordValidator.CheckArgumentIsNull(nameof(_passwordValidator));

            _usedPasswordsService = usedPasswordsService;
            _usedPasswordsService.CheckArgumentIsNull(nameof(_usedPasswordsService));

        }
        #endregion Ctor

        #region Login & LogOff
        [AllowAnonymous, NoBrowserCache]
        [BreadCrumb(Title = "ورود به سیستم", Order = 1)]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost, AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(CaptchaGeneratorLanguage = DNTCaptcha.Core.Providers.Language.Persian)]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty,
                        "نام کاربری و یا کلمه‌ی عبور وارد شده معتبر نیستند.");
                    return View(model);
                }

                if (!user.IsActive)
                {
                    ModelState.AddModelError(string.Empty, "اکانت شما غیرفعال شده‌است.");
                    return View(model);
                }

                if (_siteSettings.Value.EnableEmailConfirmation &&
                    !await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError("", "لطفا به پست الکترونیک خود مراجعه کرده و ایمیل خود را تائید کنید!");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password,
                    model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    _logger.LogInformation(1, $"{model.Username} logged in.");
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(TwoFactorController.SendCode), "TwoFactor",
                        new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, $"{model.Username} قفل شده‌است.");
                    return View("~/Areas/Identity/Views/TwoFactor/Lockout.cshtml");
                }

                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError(string.Empty, "عدم دسترسی ورود.");
                    return View(model);
                }

                ModelState.AddModelError(
                    string.Empty, "نام کاربری و یا کلمه‌ی عبور وارد شده معتبر نیستند.");
                return View(model);

            }

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> LogOff()
        {
            var user = User.Identity.IsAuthenticated ?
                await _userManager.FindByNameAsync(User.Identity.Name) : null;

            await _signInManager.SignOutAsync();

            if (user == null)
                return RedirectToAction(nameof(HomeController.Index), "Home");

            await _userManager.UpdateSecurityStampAsync(user);
            _logger.LogInformation(4, $"{user.UserName} logged out.");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        #endregion Login & LogOff

        #region Register

        [BreadCrumb(Title = "ثبت نام", Order = 1)]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(CaptchaGeneratorLanguage = DNTCaptcha.Core.Providers.Language.Persian)]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation(3, $"{user.UserName} created a new account with password.");

                    if (!_siteSettings.Value.EnableEmailConfirmation)
                        return RedirectToAction(nameof(ConfirmedRegisteration));

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //ControllerExtensions.ShortControllerName<RegisterController>(), //todo: use everywhere .................

                    await _emailSender.SendEmailAsync(
                        email: user.Email,
                        subject: "لطفا اکانت خود را تائید کنید",
                        viewNameOrPath: "~/Areas/Identity/Views/EmailTemplates/_RegisterEmailConfirmation.cshtml",
                        model: new RegisterEmailConfirmationViewModel
                        {
                            User = user,
                            EmailConfirmationToken = code,
                            EmailSignature = _siteSettings.Value.Smtp.FromName,
                            MessageDateTime = DateTime.UtcNow.ToLongPersianDateTimeString()
                        });

                    return RedirectToAction(nameof(ConfirmYourEmail));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        #endregion Register

        #region ConfirmEmail
        [BreadCrumb(Title = "تائید ایمیل", Order = 1)]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return View("Error");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return View("NotFound");


            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }

        [BreadCrumb(Title = "ایمیل خود را تائید کنید", Order = 1)]
        [AllowAnonymous]
        public IActionResult ConfirmYourEmail()
        {
            return View();
        }

        [BreadCrumb(Title = "تائیدیه ایمیل", Order = 1)]
        [AllowAnonymous]
        public IActionResult ConfirmedRegisteration()
        {
            return View();
        }
        #endregion ConfirmEmail

        #region Forgot Password
        [BreadCrumb(Title = "ایندکس", Order = 1)]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(CaptchaGeneratorLanguage = DNTCaptcha.Core.Providers.Language.Persian)]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
                {
                    return View("Error");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _emailSender.SendEmailAsync(
                   email: model.Email,
                   subject: "بازیابی کلمه‌ی عبور",
                   viewNameOrPath: "~/Areas/Identity/Views/EmailTemplates/_PasswordReset.cshtml",
                   model: new PasswordResetViewModel
                   {
                       UserId = user.Id,
                       Token = code,
                       EmailSignature = _siteSettings.Value.Smtp.FromName,
                       MessageDateTime = DateTime.UtcNow.ToLongPersianDateTimeString()
                   })
                    ;

                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        [BreadCrumb(Title = "تغییر کلمه‌ی عبور", Order = 1)]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost, AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        [BreadCrumb(Title = "تائیدیه تغییر کلمه‌ی عبور", Order = 1)]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [BreadCrumb(Title = "تائید کلمه‌ی عبور فراموش شده", Order = 1)]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        #endregion Forgot Password

        #region Change Password
        [BreadCrumb(Title = "ایندکس", Order = 1)]
        public async Task<IActionResult> ChangePassword()
        {
            var userId = User.Identity.GetUserId<int>();
            var passwordChangeDate =
                await _usedPasswordsService.GetLastUserPasswordChangeDateAsync(userId);
            return View(model: new ChangePasswordViewModel
            {
                LastUserPasswordChangeDate = passwordChangeDate
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetCurrentUserAsync();
            if (user == null)
            {
                return View("NotFound");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);

                // reflect the changes in the Identity cookie //بروزرسانی Claims
                await _signInManager.RefreshSignInAsync(user);

                await _emailSender.SendEmailAsync(
                    email: user.Email,
                    subject: "اطلاع رسانی تغییر کلمه‌ی عبور",
                    viewNameOrPath: "~/Areas/Identity/Views/EmailTemplates/_ChangePasswordNotification.cshtml",
                    model: new ChangePasswordNotificationViewModel
                    {
                        User = user,
                        EmailSignature = _siteSettings.Value.Smtp.FromName,
                        MessageDateTime = DateTime.UtcNow.ToLongPersianDateTimeString()
                    });

                return RedirectToAction("Index", "UserCard", routeValues: new { id = user.Id });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        #endregion Change Password

        #region Ajax Methods

        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ValidateUsername(string username, string email)
        {
            var result = await _userValidator.ValidateAsync((UserManager<User>)_userManager,
                new User { UserName = username, Email = email });
            return Json(result.Succeeded ? "true" : result.DumpErrors(useHtmlNewLine: true));
        }

        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ValidatePasswordByUserName(string password, string username)
        {
            var result = await _passwordValidator.ValidateAsync(
                (UserManager<User>)_userManager, new User { UserName = username }, password);
            return Json(result.Succeeded ? "true" : result.DumpErrors(useHtmlNewLine: true));
        }

        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ValidatePasswordByEmail(string password, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json("ایمیل وارد شده معتبر نیست.");
            }

            var result = await _passwordValidator.ValidateAsync(
                (UserManager<User>)_userManager, user, password);
            return Json(result.Succeeded ? "true" : result.DumpErrors(useHtmlNewLine: true));
        }

        [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ValidatePassword(string newPassword)
        {
            var user = await _userManager.GetCurrentUserAsync();
            var result = await _passwordValidator.ValidateAsync(
                (UserManager<User>)_userManager, user, newPassword);
            return Json(result.Succeeded ? "true" : result.DumpErrors(useHtmlNewLine: true));
        }
        #endregion Ajax Methods
    }
}
