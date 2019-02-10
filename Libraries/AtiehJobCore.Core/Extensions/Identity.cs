using System;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using AtiehJobCore.Core.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AtiehJobCore.Core.Extensions
{
    public static class Identity
    {
        public static void AddErrorsFromIdentityResult(this ModelStateDictionary modelState, IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError("", error.Description);
            }
        }

        /// <summary>
        /// IdentityResult errors list to string
        /// </summary>
        public static string DumpErrors(this IdentityResult result, bool useHtmlNewLine = false)
        {
            var results = new StringBuilder();

            if (result.Succeeded) return results.ToString();

            foreach (var error in result.Errors)
            {
                var errorDescription = error.Description;
                if (string.IsNullOrWhiteSpace(errorDescription))
                {
                    continue;
                }

                results.AppendLine(!useHtmlNewLine ?
                    errorDescription : $"{errorDescription}<br/>");
            }
            return results.ToString();
        }

        public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
        {
            return identity?.FindFirst(claimType)?.Value;
        }

        public static string GetUserClaimValue(this IIdentity identity, string claimType)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            return claimsIdentity?.FindFirstValue(claimType);
        }

        public static string GetUserFirstName(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.GivenName);
        }

        public static T GetUserId<T>(this IIdentity identity) where T : IConvertible
        {
            var firstValue = identity?.GetUserClaimValue(ClaimTypes.NameIdentifier);
            return firstValue != null
                ? (T)Convert.ChangeType(firstValue, typeof(T), CultureInfo.InvariantCulture)
                : default(T);
        }

        public static string GetUserId(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.NameIdentifier);
        }

        public static string GetUserLastName(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.Surname);
        }

        public static string GetUserFullName(this IIdentity identity)
        {
            return $"{GetUserFirstName(identity)} {GetUserLastName(identity)}";
        }

        public static string GetUserDisplayName(this IIdentity identity)
        {
            var fullName = GetUserFullName(identity);
            return string.IsNullOrWhiteSpace(fullName) ? GetUserName(identity) : fullName;
        }

        public static string GetUserName(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.Name);
        }

        public static string GetUserMobileNumber(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.MobilePhone);
        }

        public static string GetUserNationalCode(IIdentity identity)
        {
            return identity?.GetUserClaimValue(CustomClaimTypes.NationalCode);
        }
    }
}
