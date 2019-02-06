using AtiehJobCore.Web.Framework.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Web.Framework.Models.Admin
{
    public partial class UserSettingsModel : BaseMongoModel
    {
        public UserSettingsModel()
        {
            UserSettings = new UserSettingModel();
            DateTimeSettings = new DateTimeSettingsModel();
            ExternalAuthenticationSettings = new ExternalAuthenticationSettingsModel();
        }
        public UserSettingModel UserSettings { get; set; }
        public DateTimeSettingsModel DateTimeSettings { get; set; }
        public ExternalAuthenticationSettingsModel ExternalAuthenticationSettings { get; set; }

        #region Nested classes

        public partial class UserSettingModel : BaseMongoModel
        {
            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.UsernamesEnabled")]
            public bool UsernamesEnabled { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.AllowUsersToChangeUsernames")]
            public bool AllowUsersToChangeUsernames { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.CheckUsernameAvailabilityEnabled")]
            public bool CheckUsernameAvailabilityEnabled { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.UserRegistrationType")]
            public int UserRegistrationType { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.AllowUsersToUploadAvatars")]
            public bool AllowUsersToUploadAvatars { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.DefaultAvatarEnabled")]
            public bool DefaultAvatarEnabled { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.ShowUsersLocation")]
            public bool ShowUsersLocation { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.ShowUsersJoinDate")]
            public bool ShowUsersJoinDate { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.AllowViewingProfiles")]
            public bool AllowViewingProfiles { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.NotifyNewUserRegistration")]
            public bool NotifyNewUserRegistration { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.HideDownloadableProductsTab")]
            public bool HideDownloadableProductsTab { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.AllowUsersToDeleteAccount")]
            public bool AllowUsersToDeleteAccount { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.AllowUsersToExportData")]
            public bool AllowUsersToExportData { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.UserNameFormat")]
            public int UserNameFormat { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.PasswordMinLength")]
            public int PasswordMinLength { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.UnDuplicatedPasswordsNumber")]
            public int UnDuplicatedPasswordsNumber { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.PasswordRecoveryLinkDaysValid")]
            public int PasswordRecoveryLinkDaysValid { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.PasswordLifetime")]
            public int PasswordLifetime { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.DefaultPasswordFormat")]
            public int DefaultPasswordFormat { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.FailedPasswordAllowedAttempts")]
            public int FailedPasswordAllowedAttempts { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.FailedPasswordLockoutMinutes")]
            public int FailedPasswordLockoutMinutes { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.NewsletterEnabled")]
            public bool NewsletterEnabled { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.NewsletterTickedByDefault")]
            public bool NewsletterTickedByDefault { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.HideNewsletterBlock")]
            public bool HideNewsletterBlock { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.NewsletterBlockAllowToUnsubscribe")]
            public bool NewsletterBlockAllowToUnsubscribe { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.GenderEnabled")]
            public bool GenderEnabled { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.DateOfBirthEnabled")]
            public bool DateOfBirthEnabled { get; set; }
            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.DateOfBirthRequired")]
            public bool DateOfBirthRequired { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.DateOfBirthMinimumAge")]
            [UIHint("Int32Nullable")]
            public int? DateOfBirthMinimumAge { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.CompanyEnabled")]
            public bool CompanyEnabled { get; set; }
            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.CompanyRequired")]
            public bool CompanyRequired { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.StreetAddressEnabled")]
            public bool StreetAddressEnabled { get; set; }
            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.StreetAddressRequired")]
            public bool StreetAddressRequired { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.StreetAddress2Enabled")]
            public bool StreetAddress2Enabled { get; set; }
            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.StreetAddress2Required")]
            public bool StreetAddress2Required { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.CityEnabled")]
            public bool CityEnabled { get; set; }
            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.CityRequired")]
            public bool CityRequired { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.CountryEnabled")]
            public bool CountryEnabled { get; set; }
            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.CountryRequired")]
            public bool CountryRequired { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.StateProvinceEnabled")]
            public bool StateProvinceEnabled { get; set; }
            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.StateProvinceRequired")]
            public bool StateProvinceRequired { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.PhoneEnabled")]
            public bool PhoneEnabled { get; set; }
            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.PhoneRequired")]
            public bool PhoneRequired { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.FaxEnabled")]
            public bool FaxEnabled { get; set; }
            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.FaxRequired")]
            public bool FaxRequired { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.AcceptPrivacyPolicyEnabled")]
            public bool AcceptPrivacyPolicyEnabled { get; set; }
        }


        public partial class DateTimeSettingsModel : BaseMongoModel
        {
            public DateTimeSettingsModel()
            {
                AvailableTimeZones = new List<SelectListItem>();
            }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.AllowUsersToSetTimeZone")]
            public bool AllowUsersToSetTimeZone { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.DefaultStoreTimeZone")]
            public string DefaultStoreTimeZoneId { get; set; }

            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.DefaultStoreTimeZone")]
            public IList<SelectListItem> AvailableTimeZones { get; set; }
        }

        public partial class ExternalAuthenticationSettingsModel : BaseMongoModel
        {
            [AtiehJobResourceDisplayName("Admin.Configuration.Settings.User.ExternalAuthenticationAutoRegisterEnabled")]
            public bool AutoRegisterEnabled { get; set; }
        }
        #endregion
    }
}
