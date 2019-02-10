using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Web.Framework.Models.Account;
using AtiehJobCore.Web.Framework.Mvc.Captcha;

namespace AtiehJobCore.Web.Framework.Services
{
    public partial class UserViewModelService : IUserViewModelService
    {

        private readonly UserSettings _userSettings;
        private readonly CaptchaSettings _captchaSettings;
        public UserViewModelService(UserSettings userSettings, CaptchaSettings captchaSettings)
        {
            _userSettings = userSettings;
            _captchaSettings = captchaSettings;
        }

        public virtual LoginModel PrepareLogin()
        {
            var model = new LoginModel
            {
                UserLoginType = _userSettings.UserLoginType,
                DisplayCaptcha = _captchaSettings.Enabled && _captchaSettings.ShowOnLoginPage
            };
            return model;
        }
    }
}
