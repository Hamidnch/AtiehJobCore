using AtiehJobCore.Web.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Text;

namespace AtiehJobCore.Web.Framework.Mvc.Captcha
{
    public class GRecaptchaControl
    {
        private const string RecaptchaApiUrlVersion = "https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit";

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

            switch (_version)
            {
                case ReCaptchaVersion.Version2:
                    return RenderV2();
                case ReCaptchaVersion.Version3:
                    return RenderV3();
                default:
                    throw new NotSupportedException("Specified version is not supported");
            }
        }

        private string RenderV2()
        {
            var scriptCallbackTag = new TagBuilder("script")
            {
                TagRenderMode = TagRenderMode.Normal
            };
            scriptCallbackTag.InnerHtml
                .AppendHtml($"var onloadCallback = function() {{grecaptcha.render('{Id}', {{'sitekey' : '{PublicKey}', 'theme' : '{Theme}' }});}};");

            var captchaTag = new TagBuilder("div")
            {
                TagRenderMode = TagRenderMode.Normal
            };
            captchaTag.Attributes.Add("id", Id);

            var scriptLoadApiTag = new TagBuilder("script")
            {
                TagRenderMode = TagRenderMode.Normal
            };
            scriptLoadApiTag.Attributes.Add("src", RecaptchaApiUrlVersion + (string.IsNullOrEmpty(Language) ? "" : $"&hl={Language}"
                                                   ));
            scriptLoadApiTag.Attributes.Add("async", null);
            scriptLoadApiTag.Attributes.Add("defer", null);

            return scriptCallbackTag.RenderHtmlContent() + captchaTag.RenderHtmlContent() + scriptLoadApiTag.RenderHtmlContent();
        }

        private string RenderV3()
        {
            var scriptCallbackTag = new TagBuilder("script")
            {
                TagRenderMode = TagRenderMode.Normal
            };
            var clientId = Guid.NewGuid().ToString();
            var script = new StringBuilder();
            script.AppendLine("function onloadCallback() {");
            script.AppendLine($"var clientId = grecaptcha.render('{clientId}', {{");
            script.AppendLine($"'sitekey': '{PublicKey}',");
            script.AppendLine("'badge': 'inline',");
            script.AppendLine($"'theme': '{Theme}',");
            script.AppendLine("'size': 'invisible'");
            script.AppendLine(" });");

            script.AppendLine("grecaptcha.ready(function() {");

            script.AppendLine($" grecaptcha.execute(clientId, {{");
            script.AppendLine("   action: 'homepage'");
            script.AppendLine("  })");
            script.AppendLine("  .then(function(token) {");
            script.AppendLine($"   document.getElementById('{Id}').value = token;");
            script.AppendLine("  })");
            script.AppendLine("})}");
            scriptCallbackTag.InnerHtml.AppendHtml(script.ToString());

            var captchaTagInput = new TagBuilder("input")
            {
                TagRenderMode = TagRenderMode.Normal
            };
            captchaTagInput.Attributes.Add("type", "hidden");
            captchaTagInput.Attributes.Add("id", Id);
            captchaTagInput.Attributes.Add("name", Id);

            var captchaTagDiv = new TagBuilder("div");
            captchaTagDiv.Attributes.Add("id", $"{clientId}");

            var scriptLoadApiTag = new TagBuilder("script")
            {
                TagRenderMode = TagRenderMode.Normal
            };
            scriptLoadApiTag.Attributes.Add("src", RecaptchaApiUrlVersion + (string.IsNullOrEmpty(Language) ? "" : $"&hl={Language}"
                                                   ));

            return scriptLoadApiTag.RenderHtmlContent() + scriptCallbackTag.RenderHtmlContent()
                                                        + captchaTagInput.RenderHtmlContent() + captchaTagDiv.RenderHtmlContent();
        }

        private void SetTheme()
        {
            var themes = new[] { "white", "blackglass", "red", "clean", "light", "dark" };

            if (Theme is null)
                Theme = "light";

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
