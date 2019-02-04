using AtiehJobCore.Web.Framework.Models.Account;

namespace AtiehJobCore.Web.Framework.Services
{
    public partial interface IUserViewModelService
    {
        LoginModel PrepareLogin();
    }
}
