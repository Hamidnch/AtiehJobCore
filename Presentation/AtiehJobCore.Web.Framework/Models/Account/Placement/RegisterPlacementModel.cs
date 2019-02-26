using AtiehJobCore.Web.Framework.Models.User;
using AtiehJobCore.Web.Framework.Mvc.ModelBinding;
using AtiehJobCore.Web.Framework.Validators.Account.Placement;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Web.Framework.Models.Account.Placement
{
    /// <inheritdoc />
    /// <summary>
    /// Register placement view model
    /// </summary>
    [Validator(typeof(RegisterPlacementValidator))]
    public class RegisterPlacementModel : BaseMongoModel
    {
        public RegisterPlacementModel()
        {
            UserAttributes = new List<UserAttributeModel>();
        }
        /// <summary>
        /// Username enabled?
        /// </summary>
        public bool UsernamesEnabled { get; set; }
        public bool CheckUsernameAvailabilityEnabled { get; set; }
        [AtiehJobResourceDisplayName("Account.Employer.Fields.Username")]
        public string Username { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// National code
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Placement.Fields.NationalCode")]
        public string NationalCode { get; set; }

        /// <summary>
        /// Mobile number
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Placement.Fields.MobileNumber")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// license number
        /// شماره مجوز
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Placement.Fields.LicenseNumber")]
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [DataType(DataType.Password)]
        [AtiehJobResourceDisplayName("Account.Placement.Fields.Password")]
        public string Password { get; set; }

        /// <summary>
        /// Confirm password
        /// </summary>
        [DataType(DataType.Password)]
        [AtiehJobResourceDisplayName("Account.Placement.Fields.ConfirmPassword")]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// توافقنامه
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Fields.Agreement")]
        public bool Agreement { get; set; }

        public bool DisplayCaptcha { get; set; }
        public IList<UserAttributeModel> UserAttributes { get; set; }
        public bool HoneypotEnabled { get; set; }
        public bool AcceptPrivacyPolicyEnabled { get; set; }
    }
}
