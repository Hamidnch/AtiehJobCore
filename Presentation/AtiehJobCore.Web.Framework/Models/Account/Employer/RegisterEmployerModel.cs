using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.MongoDb;
using AtiehJobCore.Web.Framework.Models.User;
using AtiehJobCore.Web.Framework.Mvc.ModelBinding;
using AtiehJobCore.Web.Framework.Validators.Account.Employer;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Web.Framework.Models.Account.Employer
{
    [Validator(typeof(RegisterEmployerValidator))]
    public class RegisterEmployerModel : BaseMongoEntity
    {
        public RegisterEmployerModel()
        {
            UserAttributes = new List<UserAttributeModel>();
        }

        public bool UsernamesEnabled { get; set; }
        public bool CheckUsernameAvailabilityEnabled { get; set; }
        [AtiehJobResourceDisplayName("Account.Employer.Fields.Username")]
        public string Username { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Employer.Fields.Email")]
        public string Email { get; set; }
        /// <summary>
        /// National code
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Employer.Fields.NationalCode")]
        public string NationalCode { get; set; }

        /// <summary>
        /// Mobile number
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Employer.Fields.MobileNumber")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// نام شرکت/کارگاه
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Employer.Fields.CompanyName")]
        public string CompanyName { get; set; }
        /// <summary>
        /// نام مدیر
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Employer.Fields.ManagerName")]
        public string ManagerName { get; set; }
        /// <summary>
        /// کد بیمه
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Employer.Fields.InsuranceCode")]
        public string InsuranceCode { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [DataType(DataType.Password)]
        [AtiehJobResourceDisplayName("Account.Employer.Fields.Password")]
        public string Password { get; set; }

        /// <summary>
        /// Confirm password
        /// </summary>
        [DataType(DataType.Password)]
        [AtiehJobResourceDisplayName("Account.Employer.Fields.ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// توافقنامه
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Employer.Fields.Agreement")]
        public bool Agreement { get; set; }

        public UserType UserType { get; set; }
        //public bool InsuranceCodeEnabled { get; set; }
        //public bool InsuranceCodeOptional { get; set; }
        //public bool InsuranceCodeDuplicate { get; set; }
        public bool DisplayCaptcha { get; set; }
        public IList<UserAttributeModel> UserAttributes { get; set; }
        public bool HoneypotEnabled { get; set; }
        public bool AcceptPrivacyPolicyEnabled { get; set; }
    }
}
