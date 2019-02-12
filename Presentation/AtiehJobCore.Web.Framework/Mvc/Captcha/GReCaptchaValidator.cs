using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;

namespace AtiehJobCore.Web.Framework.Mvc.Captcha
{
    public class GReCaptchaValidator
    {
        private const string RecaptchaVerifyUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}&remoteip={2}";

        public string SecretKey { get; set; }
        public string RemoteIp { get; set; }
        public string Response { get; set; }
        public string Challenge { get; set; }

        private readonly ReCaptchaVersion _version;

        public GReCaptchaValidator(ReCaptchaVersion version = ReCaptchaVersion.Version2)
        {
            _version = version;
        }

        public GReCaptchaResponse Validate()
        {
            GReCaptchaResponse result;
            var httpClient = new HttpClient();

            var requestUri = string.Format(RecaptchaVerifyUrl, SecretKey, Response, RemoteIp);

            try
            {
                var taskResult = httpClient.GetAsync(requestUri);
                taskResult.Wait();
                var response = taskResult.Result;
                response.EnsureSuccessStatusCode();
                var taskString = response.Content.ReadAsStringAsync();
                taskString.Wait();
                result = ParseResponseResult(taskString.Result);

            }
            catch (Exception exc)
            {
                result = new GReCaptchaResponse { IsValid = false };
                result.ErrorCodes.Add("Unknown error" + exc.Message);
            }
            finally
            {
                httpClient.Dispose();
            }

            return result;
        }

        private static GReCaptchaResponse ParseResponseResult(string responseString)
        {
            var result = new GReCaptchaResponse();

            var resultObject = JObject.Parse(responseString);
            result.IsValid = resultObject.Value<bool>("success");

            if (resultObject.Value<JToken>("error-codes") != null &&
                    resultObject.Value<JToken>("error-codes").Values<string>().Any())
                result.ErrorCodes = resultObject.Value<JToken>("error-codes").Values<string>().ToList();

            return result;
        }
    }
}
