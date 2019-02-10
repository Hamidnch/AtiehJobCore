using AtiehJobCore.Web.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace AtiehJobCore.Web.Framework.Mvc.Captcha
{
    public class GRecaptchaControl
    {
        private const string RecaptchaApiUrlVersion2 = "https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit";

        public string Id { get; set; }
        public string Theme { get; set; }
        public string PublicKey { get; set; }
        public string Language { get; set; }

        private readonly ReCaptchaVersion _version;

        public GRecaptchaControl(ReCaptchaVersion version = ReCaptchaVersion.Version2)
        {
            _version = version;
        }

        public string RenderControl()
        {
            SetTheme();

            if (_version != ReCaptchaVersion.Version2)
            {
                throw new NotSupportedException("Specified version is not supported");
            }

            var scriptCallbackTag = new TagBuilder("script")
            {
                TagRenderMode = TagRenderMode.Normal
            };
            scriptCallbackTag.InnerHtml.AppendHtml(
                $"var onloadCallback = function() {{grecaptcha.render('{Id}', {{'sitekey' : '{PublicKey}', 'theme' : '{Theme}' }});}};");

            var captchaTag = new TagBuilder("div")
            {
                TagRenderMode = TagRenderMode.Normal
            };
            captchaTag.Attributes.Add("id", Id);

            var scriptLoadApiTag = new TagBuilder("script")
            {
                TagRenderMode = TagRenderMode.Normal
            };
            scriptLoadApiTag.Attributes.Add("src", RecaptchaApiUrlVersion2 + (string.IsNullOrEmpty(Language) ? "" : $"&hl={Language}"
                                                   ));
            scriptLoadApiTag.Attributes.Add("async", null);
            scriptLoadApiTag.Attributes.Add("defer", null);

            return scriptCallbackTag.RenderHtmlContent() + captchaTag.RenderHtmlContent() + scriptLoadApiTag.RenderHtmlContent();

        }

        private void SetTheme()
        {
            var themes = new[] { "white", "blackglass", "red", "clean", "light", "dark" };

            if (_version != ReCaptchaVersion.Version2)
            {
                return;
            }

            switch (Theme.ToLower())
            {
                case "clean":
                case "red":
                case "white":
                    Theme = "light";
                    break;
                case "blackglass":
                    Theme = "dark";
                    break;
                default:
                    if (!themes.Contains(Theme.ToLower()))
                    {
                        Theme = "light";
                    }
                    break;
            }
        }
    }
}
