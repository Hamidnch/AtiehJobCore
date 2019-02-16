using AtiehJobCore.Core.Enums;
using AtiehJobCore.Web.Framework.Mvc.ModelBinding;
using AtiehJobCore.Web.Framework.Validators.Account.Jobseeker;
using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Web.Framework.Models.Account.Jobseeker
{
    [Validator(typeof(RegisterSimpleJobseekerModelValidator))]
    public partial class RegisterSimpleJobseekerModel : BaseMongoModel
    {
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
        //[Required(ErrorMessage = "Account.Jobseeker.Fields.NationalCode.Required")]
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.NationalCode")]
        public string NationalCode { get; set; }

        /// <summary>
        /// Mobile number
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.MobileNumber")]
        //[RegularExpression("09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}",
        //    ErrorMessage = "Account.Jobseeker.Fields.WrongMobileNumber")]
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
    }
}
