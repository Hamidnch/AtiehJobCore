using AtiehJobCore.Common.Constants;
using AtiehJobCore.Common.Extensions;
using AtiehJobCore.Services.Identity.Interfaces;
using AtiehJobCore.ViewModel.Models.Identity.Emails;
using AtiehJobCore.ViewModel.Models.Identity.Password;
using AtiehJobCore.ViewModel.Models.Identity.Settings;
using DNTBreadCrumb.Core;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace AtiehJobCore.Web.Areas.Identity.Controllers
{
    [Authorize]
    [Area(AreaNames.Identity)]
    [BreadCrumb(Title = "اعتبارسنجی دو مرحله‌ای", UseDefaultRouteUrl = true, Order = 0)]
    public class TwoFactorController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<TwoFactorController> _logger;
        private readonly ISignInManager _signInManager;
        private readonly IUserManager _userManager;
        private readonly IOptionsSnapshot<SiteSettings> _siteSettings;

        public TwoFactorController(IEmailSender emailSender, ILogger<TwoFactorController> logger,
            ISignInManager signInManager, IUserManager userManager, IOptionsSnapshot<SiteSettings> siteSettings)
        {
            _userManager = userManager;
            _userManager.CheckArgumentIsNull(nameof(_userManager));

            _signInManager = signInManager;
            _signInManager.CheckArgumentIsNull(nameof(_signInManager));

            _emailSender = emailSender;
            _emailSender.CheckArgumentIsNull(nameof(_emailSender));

            _siteSettings = siteSettings;
            _siteSettings.CheckArgumentIsNull(nameof(_siteSettings));

            _logger = logger;
            _logger.CheckArgumentIsNull(nameof(_logger));
        }

        [AllowAnonymous]
        [BreadCrumb(Title = "ارسال کد", Order = 1)]
        public async Task<IActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("NotFound");
            }

            const string tokenProvider = "Email";
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, tokenProvider);
            if (string.IsNullOrWhiteSpace(code))
            {
                return View("Error");
            }

            await _emailSender.SendEmailAsync(
                               email: user.Email,
                               subject: "کد جدید اعتبارسنجی دو مرحله‌ای",
                               viewNameOrPath: "~/Areas/Identity/Views/EmailTemplates/_TwoFactorSendCode.cshtml",
                               model: new TwoFactorSendCodeViewModel
                               {
                                   Token = code,
                                   EmailSignature = _siteSettings.Value.Smtp.FromName,
                                   MessageDateTime = DateTime.UtcNow.ToLongPersianDateTimeString()
                               });

            return RedirectToAction(nameof(VerifyCode),
                new
                {
                    Provider = tokenProvider,
                    ReturnUrl = returnUrl,
                    RememberMe = rememberMe
                });
        }

        [AllowAnonymous]
        [BreadCrumb(Title = "تائید کد", Order = 1)]
        public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
        {
            // Require that the user has already logged in via username/password or external login
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            return user == null ?
                View("NotFound") :
                View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            var result = await _signInManager.TwoFactorSignInAsync(
                model.Provider,
                model.Code,
                model.RememberMe,
                model.RememberBrowser);

            if (result.Succeeded)
            {
                if (Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning(7, "User account locked out.");
                return View("Lockout");
            }

            ModelState.AddModelError(string.Empty, "کد وارد شده غیر معتبر است.");
            return View(model);
        }
    }
}