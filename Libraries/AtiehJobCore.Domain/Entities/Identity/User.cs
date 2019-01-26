using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.Enums;
using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.Employers;
using AtiehJobCore.Domain.Entities.Identity.Plus;
using AtiehJobCore.Domain.Entities.Jobseekers;
using AtiehJobCore.Domain.Entities.Payments;
using AtiehJobCore.Domain.Entities.Placements;
using Microsoft.AspNetCore.Identity;

namespace AtiehJobCore.Domain.Entities.Identity
{
    public class User : IdentityUser<int>, IAuditableEntity
    {
        #region Ctor
        public User()
        {
            UserUsedPasswords = new HashSet<UserUsedPassword>();
            UserTokens = new HashSet<UserToken>();
        }

        #endregion Ctor

        #region Properties

        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(450)]
        public string LastName { get; set; }
        [NotMapped]
        public string DisplayName
        {
            get
            {
                var displayName = $"{FirstName} {LastName}";
                return string.IsNullOrWhiteSpace(displayName) ? UserName : displayName;
            }
        }
        [StringLength(100)]
        public string FatherName { get; set; }
        [StringLength(11)]
        public string MobileNumber { get; set; }
        [StringLength(10)]
        public string NationalCode { get; set; }
        [StringLength(100)]
        public string SsnId { get; set; }
        [StringLength(100)]
        public string SsnSerial { get; set; }
        public GenderType? GenderType { get; set; }
        [StringLength(450)]
        public string PhotoFileName { get; set; }

        public DateTimeOffset? DateOfBirth { get; set; }
        public DateTimeOffset? CreatedDateTime { get; set; }
        public DateTimeOffset? LastVisitedDateTime { get; set; }
        [StringLength(150)]
        public string PlaceOfBirth { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmailPublic { get; set; }
        public bool IsDeleted { get; set; }
        public virtual UserType UserType { get; set; }


        #region NavigationProperties
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserUsedPassword> UserUsedPasswords { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Jobseeker> Jobseekers { get; set; }
        public virtual ICollection<Employer> Employers { get; set; }
        public virtual ICollection<Placement> Placements { get; set; }
        public virtual ICollection<UserAccountCharge> UserAccountCharges { get; set; }

        #endregion NavigationProperties

        #endregion Properties
    }
}