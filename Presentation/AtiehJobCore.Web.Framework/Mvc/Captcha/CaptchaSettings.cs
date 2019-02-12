using AtiehJobCore.Core.Configuration;

namespace AtiehJobCore.Web.Framework.Mvc.Captcha
{
    /// <inheritdoc />
    /// <summary>
    /// CAPTCHA settings
    /// </summary>
    public class CaptchaSettings : ISettings
    {
        /// <summary>
        /// Is CAPTCHA enabled?
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// A value indicating whether CAPTCHA should be displayed on the login page
        /// </summary>
        public bool ShowOnLoginPage { get; set; }
        /// <summary>
        /// A value indicating whether CAPTCHA should be displayed on the registration page
        /// </summary>
        public bool ShowOnRegistrationPage { get; set; }
        /// <summary>
        /// A value indicating whether CAPTCHA should be displayed on the contacts page
        /// </summary>
        public bool ShowOnContactUsPage { get; set; }
        /// <summary>
        /// reCAPTCHA public key
        /// </summary>
        public string ReCaptchaPublicKey { get; set; }
        /// <summary>
        /// reCAPTCHA private key
        /// </summary>
        public string ReCaptchaPrivateKey { get; set; }
        /// <summary>
        /// reCAPTCHA theme
        /// </summary>
        public string ReCaptchaTheme { get; set; }
        /// <summary>
        /// reCAPTCHA version
        /// </summary>
        public ReCaptchaVersion ReCaptchaVersion { get; set; }
        /// <summary>
        /// reCAPTCHA language
        /// </summary>
        public string ReCaptchaLanguage { get; set; }
    }
}
