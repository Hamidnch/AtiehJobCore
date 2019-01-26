using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AtiehJobCore.Common.Contracts;
using Microsoft.AspNetCore.Identity;

namespace AtiehJobCore.Domain.Entities.Identity
{
    public class Role : IdentityRole<int>, IAuditableEntity
    {
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }

        public Role()
        {
        }

        public Role(string name)
        {
            Name = name;
        }
        public Role(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public Role(string name, bool isActive, string description)
        {
            Name = name;
            IsActive = isActive;
            Description = description;
        }

        /// <summary>
        /// Navigation Properties
        /// </summary>
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RoleClaim> RoleClaims { get; set; }
    }
}