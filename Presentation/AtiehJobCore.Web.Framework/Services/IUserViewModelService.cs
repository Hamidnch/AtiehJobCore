using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Web.Framework.Models.Account;
using AtiehJobCore.Web.Framework.Models.Account.Employer;
using AtiehJobCore.Web.Framework.Models.Account.Jobseeker;
using AtiehJobCore.Web.Framework.Models.User;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AtiehJobCore.Web.Framework.Services
{
    public partial interface IUserViewModelService
    {
        LoginModel PrepareLoginModel();
        RegisterJobseekerModel PrepareRegisterJobseekerModel(RegisterJobseekerModel model, bool excludeProperties,
            string overrideCustomUserAttributesXml = "");
        RegisterEmployerModel PrepareRegisterEmployerModel(RegisterEmployerModel model, bool excludeProperties,
            string overrideCustomUserAttributesXml = "");
        IList<UserAttributeModel> PrepareCustomAttributes(User user, string overrideAttributesXml = "");
        string ParseCustomAttributes(IFormCollection form);
    }
}
