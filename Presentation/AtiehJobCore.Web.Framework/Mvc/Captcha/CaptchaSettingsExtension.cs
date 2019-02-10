using AtiehJobCore.Services.Localization;

namespace AtiehJobCore.Web.Framework.Mvc.Captcha
{
    public static class CaptchaSettingsExtension
    {
        public static string GetWrongCaptchaMessage(this CaptchaSettings captchaSettings,
            ILocalizationService localizationService)
        {
            return captchaSettings.ReCaptchaVersion == ReCaptchaVersion.Version2
                ? localizationService.GetResource("Common.WrongCaptchaV2") : string.Empty;
        }
    }
}
