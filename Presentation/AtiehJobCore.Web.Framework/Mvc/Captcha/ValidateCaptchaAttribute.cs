using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace AtiehJobCore.Web.Framework.Mvc.Captcha
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a filter attribute enabling CAPTCHA validation
    /// </summary>
    public class ValidateCaptchaAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Create instance of the filter attribute 
        /// </summary>
        /// <param name="actionParameterName">The name of the action parameter to which the result will be passed</param>
        public ValidateCaptchaAttribute(string actionParameterName = "captchaValid")
            : base(typeof(ValidateCaptchaFilter))
        {
            Arguments = new object[] { actionParameterName };
        }

        #region Nested filter

        /// <inheritdoc />
        /// <summary>
        /// Represents a filter enabling CAPTCHA validation
        /// </summary>
        private class ValidateCaptchaFilter : IActionFilter
        {
            #region Constants

            private const string ChallengeFieldKey = "recaptcha_challenge_field";
            private const string ResponseFieldKey = "recaptcha_response_field";
            private const string GResponseFieldKey = "g-recaptcha-response";

            #endregion

            #region Fields

            private readonly string _actionParameterName;
            private readonly CaptchaSettings _captchaSettings;

            #endregion

            #region Ctor

            public ValidateCaptchaFilter(string actionParameterName, CaptchaSettings captchaSettings)
            {
                _actionParameterName = actionParameterName;
                _captchaSettings = captchaSettings;
            }

            #endregion

            #region Utilities

            /// <summary>
            /// Validate CAPTCHA
            /// </summary>
            /// <param name="context">A context for action filters</param>
            /// <returns>True if CAPTCHA is valid; otherwise false</returns>
            private bool ValidateCaptcha(ActionExecutingContext context)
            {
                var isValid = false;

                //get form values
                var captchaChallengeValue = context.HttpContext.Request.Form[ChallengeFieldKey];
                var captchaResponseValue = context.HttpContext.Request.Form[ResponseFieldKey];
                var gCaptchaResponseValue = context.HttpContext.Request.Form[GResponseFieldKey];

                if ((!StringValues.IsNullOrEmpty(captchaChallengeValue) && !StringValues.IsNullOrEmpty(captchaResponseValue)) || !StringValues.IsNullOrEmpty(gCaptchaResponseValue))
                {
                    //create CAPTCHA validator
                    var captchaValidator = new GReCaptchaValidator(_captchaSettings.ReCaptchaVersion)
                    {
                        SecretKey = _captchaSettings.ReCaptchaPrivateKey,
                        RemoteIp = context.HttpContext.Connection.RemoteIpAddress?.ToString(),
                        Response = !StringValues.IsNullOrEmpty(captchaResponseValue) ? captchaResponseValue : gCaptchaResponseValue,
                        Challenge = captchaChallengeValue
                    };

                    //validate request
                    var recaptchaResponse = captchaValidator.Validate();
                    isValid = recaptchaResponse.IsValid;
                }

                return isValid;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Called before the action executes, after model binding is complete
            /// </summary>
            /// <param name="context">A context for action filters</param>
            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (context == null)
                    return;

                //whether CAPTCHA is enabled
                if (_captchaSettings.Enabled && context.HttpContext != null && context.HttpContext.Request != null)
                {
                    //push the validation result as an action parameter
                    context.ActionArguments[_actionParameterName] = ValidateCaptcha(context);
                }
                else
                    context.ActionArguments[_actionParameterName] = false;

            }

            /// <inheritdoc />
            /// <summary>
            /// Called after the action executes, before the action result
            /// </summary>
            /// <param name="context">A context for action filters</param>
            public void OnActionExecuted(ActionExecutedContext context)
            {
                //do nothing
            }

            #endregion
        }

        #endregion
    }
}
