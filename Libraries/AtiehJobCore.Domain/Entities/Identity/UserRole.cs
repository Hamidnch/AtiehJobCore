using AtiehJobCore.Common.Contracts;
using Microsoft.AspNetCore.Identity;

namespace AtiehJobCore.Domain.Entities.Identity
{
    public class UserRole : IdentityUserRole<int>, IAuditableEntity
    {
        /// <summary>
        /// Navigation Properties
        /// </summary>
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}