using AtiehJobCore.Common.Enums;
using AtiehJobCore.Common.Enums.MongoDb;
using AtiehJobCore.Common.MongoDb.Domain.Common;
using AtiehJobCore.Common.MongoDb.Domain.Employers;
using AtiehJobCore.Common.MongoDb.Domain.Jobseekers;
using AtiehJobCore.Common.MongoDb.Domain.Payments;
using AtiehJobCore.Common.MongoDb.Domain.Placements;
using System;
using System.Collections.Generic;

namespace AtiehJobCore.Common.MongoDb.Domain.Users
{
    public partial class User : BaseMongoEntity
    {
        public User()
        {
            UserGuid = new Guid();
            PasswordFormat = PasswordFormat.Clear;
            //UserHistoryPasswords = new HashSet<UserHistoryPassword>();
        }
        public Guid UserGuid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsEmailPublic { get; set; }
        public string Password { get; set; }
        public int PasswordFormatId { get; set; }
        public string PasswordSalt { get; set; }
        public PasswordFormat PasswordFormat
        {
            get => (PasswordFormat)PasswordFormatId;
            set => PasswordFormatId = (int)value;
        }
        public GenderType? GenderType { get; set; }
        public UserType UserType { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string NationalCode { get; set; }
        //public string SsnId { get; set; }
        //public string SsnSerial { get; set; }
        //public string PlaceOfBirth { get; set; }
        public string PhotoFileName { get; set; }
        //public string FatherName { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public bool IsSystemAccount { get; set; }
        public int FailedLoginAttempts { get; set; }
        public string SystemName { get; set; }
        public string LastIpAddress { get; set; }
        public string UrlReferrer { get; set; }
        public string AdminComment { get; set; }
        //public DateTime? DateOfBirth { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? LastLoginDateUtc { get; set; }
        public DateTime LastActivityDateUtc { get; set; }
        public DateTime? PasswordChangeDateUtc { get; set; }
        public DateTime? CannotLoginUntilDateUtc { get; set; }

        #region Navigation Properties

        private ICollection<Role> _roles;
        public virtual ICollection<Role> Roles
        {
            get => _roles ?? (_roles = new List<Role>());
            protected set => _roles = value;
        }

        //public virtual ICollection<UserHistoryPassword> UserHistoryPasswords { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Jobseeker> Jobseekers { get; set; }
        public virtual ICollection<Employer> Employers { get; set; }
        public virtual ICollection<Placement> Placements { get; set; }
        public virtual ICollection<UserAccountCharge> UserAccountCharges { get; set; }

        #endregion Navigation Property
    }
}
