using AtiehJobCore.Core.Domain.Common;
using AtiehJobCore.Core.Domain.Employers;
using AtiehJobCore.Core.Domain.Jobseekers;
using AtiehJobCore.Core.Domain.Payments;
using AtiehJobCore.Core.Domain.Placements;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.MongoDb;
using System;
using System.Collections.Generic;

namespace AtiehJobCore.Core.Domain.Users
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
            get => (PasswordFormat)this.PasswordFormatId;
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
        private ICollection<Jobseeker> _jobseekers;
        private ICollection<Employer> _employers;
        private ICollection<Placement> _placements;
        private ICollection<Attachment> _attachments;
        private ICollection<UserAccountCharge> _userAccountCharges;

        public virtual ICollection<Role> Roles
        {
            get => _roles ?? (_roles = new List<Role>());
            protected set => _roles = value;
        }
        public virtual ICollection<Jobseeker> Jobseekers
        {
            get => _jobseekers ?? (_jobseekers = new List<Jobseeker>());
            protected set => _jobseekers = value;
        }
        public virtual ICollection<Employer> Employers
        {
            get => _employers ?? (_employers = new List<Employer>());
            protected set => _employers = value;
        }
        public virtual ICollection<Placement> Placements
        {
            get => _placements ?? (_placements = new List<Placement>());
            protected set => _placements = value;
        }
        public virtual ICollection<Attachment> Attachments
        {
            get => _attachments ?? (_attachments = new List<Attachment>());
            protected set => _attachments = value;
        }
        public virtual ICollection<UserAccountCharge> UserAccountCharges
        {
            get => _userAccountCharges ?? (_userAccountCharges = new List<UserAccountCharge>());
            protected set => _userAccountCharges = value;
        }

        #endregion Navigation Property
    }
}
