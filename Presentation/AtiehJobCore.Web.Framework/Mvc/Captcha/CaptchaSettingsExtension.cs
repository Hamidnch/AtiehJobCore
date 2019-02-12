using AtiehJobCore.Services.Localization;

namespace AtiehJobCore.Web.Framework.Mvc.Captcha
{
    public static class CaptchaSettingsExtension
    {
        public static string GetWrongCaptchaMessage(this CaptchaSettings captchaSettings,
            ILocalizationService localizationService)
        {
            switch (captchaSettings.ReCaptchaVersion)
            {
                case ReCaptchaVersion.Version2:
                    return localizationService.GetResource("Common.WrongCaptchaV2");
                case ReCaptchaVersion.Version3:
                    return localizationService.GetResource("Common.WrongCaptchaV3");
                default:
                    return string.Empty;
            }
        }
    }
}
