using System;
using System.Linq;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Core.Utilities;
using AtiehJobCore.Services.Common;
using AtiehJobCore.Services.Localization;

namespace AtiehJobCore.Services.Extensions
{
    public static class UserExtensions
    {
        /// <summary>
        /// Get full name
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>User full name</returns>
        public static string GetFullName(this User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            var firstName = user.GetAttribute<string>(SystemUserAttributeNames.FirstName);
            var lastName = user.GetAttribute<string>(SystemUserAttributeNames.LastName);

            var fullName = "";
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
                fullName = $"{firstName} {lastName}";
            else
            {
                if (!string.IsNullOrWhiteSpace(firstName))
                    fullName = firstName;

                if (!string.IsNullOrWhiteSpace(lastName))
                    fullName = lastName;
            }
            return fullName;
        }
        /// <summary>
        /// Formats the user name
        /// </summary>
        /// <param name="user">Source</param>
        /// <param name="stripTooLong">Strip too long user name</param>
        /// <param name="maxLength">Maximum user name length</param>
        /// <returns>Formatted text</returns>
        public static string FormatUserName(this User user, bool stripTooLong = false, int maxLength = 0)
        {
            if (user == null)
                return string.Empty;

            if (user.IsGuest())
            {
                return EngineContext.Current.Resolve<ILocalizationService>().GetResource("User.Guest");
            }

            string result;
            switch (EngineContext.Current.Resolve<UserSettings>().UserNameFormat)
            {
                case UserNameFormat.ShowEmails:
                    result = user.Email;
                    break;
                case UserNameFormat.ShowUsernames:
                    result = user.Username;
                    break;
                case UserNameFormat.ShowFullNames:
                    result = user.GetFullName();
                    break;
                case UserNameFormat.ShowFirstName:
                    result = user.GetAttribute<string>(SystemUserAttributeNames.FirstName);
                    break;
                case UserNameFormat.ShowMobileNumber:
                    result = user.MobileNumber;
                    break;
                case UserNameFormat.ShowNationalCode:
                    result = user.NationalCode;
                    break;
                default:
                    result = string.Empty;
                    break;
            }

            if (stripTooLong && maxLength > 0)
            {
                result = CommonHelper.EnsureMaximumLength(result, maxLength);
            }

            return result;
        }

        /// <summary>
        /// Check whether password recovery token is valid
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="token">Token to validate</param>
        /// <returns>Result</returns>
        public static bool IsPasswordRecoveryTokenValid(this User user, string token)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var passwordRecoveryAttribute = user.GetAttribute<string>(SystemUserAttributeNames.PasswordRecoveryToken);
            return !string.IsNullOrEmpty(passwordRecoveryAttribute) && passwordRecoveryAttribute.Equals(token, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Check whether password recovery link is expired
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="userSettings">User settings</param>
        /// <returns>Result</returns>
        public static bool IsPasswordRecoveryLinkExpired(this User user, UserSettings userSettings)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (userSettings == null)
                throw new ArgumentNullException(nameof(userSettings));

            if (userSettings.PasswordRecoveryLinkDaysValid == 0)
                return false;

            var generatedDate = user.GetAttribute<DateTime?>(SystemUserAttributeNames.PasswordRecoveryTokenDateGenerated);
            if (!generatedDate.HasValue)
                return false;

            var daysPassed = (DateTime.UtcNow - generatedDate.Value).TotalDays;
            return daysPassed > userSettings.PasswordRecoveryLinkDaysValid;
        }

        /// <summary>
        /// Get user role identifiers
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="showHidden">A value indicating whether to load hidden records</param>
        /// <returns>User role identifiers</returns>
        public static string[] GetUserRoleIds(this User user, bool showHidden = false)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userRolesIds = user.Roles
               .Where(cr => showHidden || cr.Active)
               .Select(cr => cr.Id)
               .ToArray();

            return userRolesIds;
        }

        /// <summary>
        /// Check whether user password is expired 
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>True if password is expired; otherwise false</returns>
        public static bool PasswordIsExpired(this User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            //the guests don't have a password
            if (user.IsGuest())
                return false;

            //password lifetime is disabled for user
            if (!user.Roles.Any(role => role.Active && role.EnablePasswordLifetime))
                return false;

            //setting disabled for all
            var userSettings = EngineContext.Current.Resolve<UserSettings>();
            if (userSettings.PasswordLifetime == 0)
                return false;

            var currentLifetime = !user.PasswordChangeDateUtc.HasValue
                ? int.MaxValue
                : (DateTime.UtcNow - user.PasswordChangeDateUtc.Value).Days;

            return currentLifetime >= userSettings.PasswordLifetime;
        }
    }
}
