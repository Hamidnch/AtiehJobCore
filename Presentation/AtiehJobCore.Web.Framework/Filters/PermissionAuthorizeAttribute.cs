using Microsoft.AspNetCore.Authorization;

namespace AtiehJobCore.Web.Framework.Filters
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        internal const string PolicyPrefix = "PERMISSION:";

        /// <inheritdoc />
        /// <summary>
        /// Creates a new instance of <see cref="T:Microsoft.AspNetCore.Authorization.AuthorizeAttribute" /> class.
        /// </summary>
        /// <param name="permissions">A list of permissions to authorize</param>
        public PermissionAuthorizeAttribute(params string[] permissions)
        {
            Policy = $"{PolicyPrefix}{string.Join(",", permissions)}";
        }
    }
}
