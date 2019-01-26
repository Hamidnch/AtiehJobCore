﻿using AtiehJobCore.Common.Contracts;
using Microsoft.AspNetCore.Identity;

namespace AtiehJobCore.Domain.Entities.Identity
{
    public class UserToken : IdentityUserToken<int>, IAuditableEntity
    {
        /// <summary>
        /// Navigation Properties
        /// </summary>
        public virtual User User { get; set; }
    }
}