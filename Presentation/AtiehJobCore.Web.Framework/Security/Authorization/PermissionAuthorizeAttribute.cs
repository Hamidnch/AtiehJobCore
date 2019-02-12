using System;
using Microsoft.AspNetCore.Authorization;

namespace AtiehJobCore.Web.Framework.Security.Authorization
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        private const string PolicyPrefix = "Permission";

        public PermissionAuthorizeAttribute(string permission) => Permission = permission;

        // Get or set the permission property by manipulating the underlying Policy property
        public string Permission
        {
            get =>
                Policy.StartsWith(PolicyPrefix, StringComparison.OrdinalIgnoreCase)
                    ? Policy.Replace(PolicyPrefix, "")
                    : string.Empty;
            set => Policy = $"{PolicyPrefix}{value.ToString()}";
        }
    }
}
