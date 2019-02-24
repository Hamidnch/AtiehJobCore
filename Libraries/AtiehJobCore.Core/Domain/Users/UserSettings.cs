using AtiehJobCore.Core.Configuration;
using AtiehJobCore.Core.Enums;

namespace AtiehJobCore.Core.Domain.Users
{
    public class UserSettings : ISettings
    {
        //public bool EmailEnabled { get; set; }
        //public bool UsernamesEnabled { get; set; }
        //public bool MobileNumberEnabled { get; set; }
        //public bool NationalCodeEnabled { get; set; }
        //public UserLoginType UserLoginType { get; set; } = UserLoginType.Email;

        public bool UsernamesEnabled { get; set; }

        public bool IsDisplayEmail { get; set; }
        public bool IsDisplayMobileNumber { get; set; }
        public bool IsDisplayNationalCode { get; set; }

        public bool IsDisplayPassword { get; set; }

        public bool IsOptionalEmail { get; set; }
        public bool IsOptionalMobileNumber { get; set; }
        public bool IsOptionalNationalCode { get; set; }

        public bool ForceEmailValidation { get; set; }
        public bool ForceMobileNumberValidation { get; set; }
        public bool ForceNationalCodeValidation { get; set; }

        public bool AllowDuplicateEmail { get; set; }
        public bool AllowDuplicateMobileNumber { get; set; }
        public bool AllowDuplicateNationalCode { get; set; }

        public bool IsSendRegisterationSms { get; set; }
        public bool IsSendRegisterationEmail { get; set; }
        public bool IsSendPasswordSms { get; set; }
        public bool IsSendPasswordEmail { get; set; }

        public bool IsAgreement { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether users can check the availability of usernames (when registering or changing in 'My Account')
        /// </summary>
        public bool CheckUsernameAvailabilityEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether users are allowed to change their usernames
        /// </summary>
        public bool AllowUsersToChangeUsernames { get; set; }

        /// <summary>
        /// Default password format for users
        /// </summary>
        public PasswordFormat DefaultPasswordFormat { get; set; }

        /// <summary>
        /// Gets or sets a user password format (SHA1, MD5) when passwords are hashed
        /// </summary>
        public string HashedPasswordFormat { get; set; }

        /// <summary>
        /// Gets or sets a minimum password length
        /// </summary>
        public int PasswordMinLength { get; set; }

        /// <summary>
        /// Gets or sets a number of passwords that should not be the same as the previous one; 0 if the user can use the same password time after time
        /// </summary>
        public int UnDuplicatedPasswordsNumber { get; set; }

        /// <summary>
        /// Gets or sets a number of days for password recovery link. Set to 0 if it doesn't expire.
        /// </summary>
        public int PasswordRecoveryLinkDaysValid { get; set; }

        /// <summary>
        /// User registration type
        /// </summary>
        public UserRegistrationType UserRegistrationType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether users are allowed to upload avatars.
        /// </summary>
        public bool AllowUsersToUploadAvatars { get; set; }

        /// <summary>
        /// Gets or sets a maximum avatar size (in bytes)
        /// </summary>
        public int AvatarMaximumSizeBytes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to display default user avatar.
        /// </summary>
        public bool DefaultAvatarEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether users location is shown
        /// </summary>
        public bool ShowUsersLocation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show users join date
        /// </summary>
        public bool ShowUsersJoinDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether users are allowed to view profiles of other users
        /// </summary>
        public bool AllowViewingProfiles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'New user' notification message should be sent to a store owner
        /// </summary>
        public bool NotifyNewUserRegistration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to hide 'Downloadable products' tab on 'My account' page
        /// </summary>
        public bool HideDownloadableProductsTab { get; set; }

        /// <summary>
        /// User name formatting
        /// </summary>
        public UserNameFormat UserNameFormat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Newsletter' form field is enabled
        /// </summary>
        public bool NewsletterEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Newsletter' checkbox is ticked by default on the registration page
        /// </summary>
        public bool NewsletterTickedByDefault { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to hide newsletter box
        /// </summary>
        public bool HideNewsletterBlock { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether newsletter block should allow to unsubscribe
        /// </summary>
        public bool NewsletterBlockAllowToUnsubscribe { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the number of minutes for 'online users' module
        /// </summary>
        public int OnlineUserMinutes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating we should store last visited page URL for each user
        /// </summary>
        public bool StoreLastVisitedPage { get; set; }

        /// <summary>
        /// For the reasons of efficiency it is required to set, besides settings in activity Log - PublicStore.Url
        /// </summary>
        public bool SaveVisitedPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether deleted user records should be prefixed suffixed with "-DELETED"
        /// </summary>
        public bool SuffixDeletedUsers { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether users are allowed to delete account
        /// </summary>
        public bool AllowUsersToDeleteAccount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether users are allowed to export account
        /// </summary>
        public bool AllowUsersToExportData { get; set; }

        /// <summary>
        /// Gets or sets maximum login failures to lockout account. Set 0 to disable this feature
        /// </summary>
        public int FailedPasswordAllowedAttempts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int FailedPasswordLockoutMinutes { get; set; }

        /// <summary>
        /// Gets or sets a number of days for password expiration
        /// </summary>
        public int PasswordLifetime { get; set; }

        #region Form fields

        /// <summary>
        /// Gets or sets a value indicating whether 'Gender' is enabled
        /// </summary>
        public bool GenderEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Date of Birth' is enabled
        /// </summary>
        public bool DateOfBirthEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether 'Date of Birth' is required
        /// </summary>
        public bool DateOfBirthRequired { get; set; }

        /// <summary>
        /// Gets or sets a minimum age. Null if ignored
        /// </summary>
        public int? DateOfBirthMinimumAge { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Company' is enabled
        /// </summary>
        public bool CompanyEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether 'Company' is required
        /// </summary>
        public bool CompanyRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Street address' is enabled
        /// </summary>
        public bool StreetAddressEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether 'Street address' is required
        /// </summary>
        public bool StreetAddressRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Street address 2' is enabled
        /// </summary>
        public bool StreetAddress2Enabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether 'Street address 2' is required
        /// </summary>
        public bool StreetAddress2Required { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Zip / postal code' is enabled
        /// </summary>
        public bool ZipPostalCodeEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether 'Zip / postal code' is required
        /// </summary>
        public bool ZipPostalCodeRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'City' is enabled
        /// </summary>
        public bool CityEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether 'City' is required
        /// </summary>
        public bool CityRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Country' is enabled
        /// </summary>
        public bool CountryEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether 'Country' is required
        /// </summary>
        public bool CountryRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'State / province' is enabled
        /// </summary>
        public bool StateProvinceEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether 'State / province' is required
        /// </summary>
        public bool StateProvinceRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Phone number' is enabled
        /// </summary>
        public bool PhoneEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether 'Phone number' is required
        /// </summary>
        public bool PhoneRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 'Fax number' is enabled
        /// </summary>
        public bool FaxEnabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether 'Fax number' is required
        /// </summary>
        public bool FaxRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether privacy policy should accepted during registration
        /// </summary>
        public bool AcceptPrivacyPolicyEnabled { get; set; }

        #endregion
    }
}
