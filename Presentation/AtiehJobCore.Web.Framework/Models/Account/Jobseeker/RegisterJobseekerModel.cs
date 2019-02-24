using AtiehJobCore.Core.Enums;
using AtiehJobCore.Web.Framework.Models.User;
using AtiehJobCore.Web.Framework.Mvc.ModelBinding;
using AtiehJobCore.Web.Framework.Validators.Account.Jobseeker;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Web.Framework.Models.Account.Jobseeker
{
    [Validator(typeof(RegisterJobseekerModelValidator))]
    public partial class RegisterJobseekerModel : BaseMongoModel
    {
        public RegisterJobseekerModel()
        {
            UserAttributes = new List<UserAttributeModel>();
        }
        /// <summary>
        /// First name
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.FirstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Family name
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Username enabled?
        /// </summary>
        public bool UsernamesEnabled { get; set; }
        public bool CheckUsernameAvailabilityEnabled { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Username")]
        public string Username { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// National code
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.NationalCode")]
        //[Remote("IsAllNationalCodeExist", nameof(Common), "", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "DuplicateNationalCode", HttpMethod = "POST")]
        public string NationalCode { get; set; }

        /// <summary>
        /// Mobile number
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.MobileNumber")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [DataType(DataType.Password)]
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Password")]
        public string Password { get; set; }

        /// <summary>
        /// Confirm password
        /// </summary>
        [DataType(DataType.Password)]
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gender enabled?
        /// </summary>
        public bool GenderEnabled { get; set; }

        /// <summary>
        /// Gender(man or female)
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Gender")]
        public GenderType? Gender { get; set; }
        /// <summary>
        /// توافقنامه
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Employer.Fields.Agreement")]
        public bool Agreement { get; set; }

        public bool DisplayCaptcha { get; set; }
        public IList<UserAttributeModel> UserAttributes { get; set; }
        public bool HoneypotEnabled { get; set; }
        public bool AcceptPrivacyPolicyEnabled { get; set; }
    }
}
