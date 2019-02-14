//using AtiehJobCore.Core.Contracts;
//using AtiehJobCore.Core.Enums;
//using DNTCommon.Web.Core;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using System;
//using System.ComponentModel.DataAnnotations;

//namespace AtiehJobCore.Core.Domain.Common
//{
//    public class SiteSettings : IValidatable
//    {
//        public AdminUserSeed AdminUserSeed { get; set; }
//        public Logging Logging { get; set; }
//        public SmtpConfig Smtp { get; set; }
//        public ConnectionStrings ConnectionStrings { get; set; }
//        public bool EnableEmailConfirmation { get; set; }
//        public TimeSpan EmailConfirmationTokenProviderLifespan { get; set; }
//        public int NotAllowedPreviouslyUsedPasswords { get; set; }
//        public int ChangePasswordReminderDays { get; set; }
//        public PasswordOptions PasswordOptions { get; set; }
//        public ActiveDatabase ActiveDatabase { get; set; }
//        public string UsersAvatarsFolder { get; set; }
//        public string UserDefaultPhoto { get; set; }
//        public string ContentSecurityPolicyErrorLogUri { get; set; }
//        public CookieOptions CookieOptions { get; set; }
//        public LockoutOptions LockoutOptions { get; set; }
//        public UserAvatarImageOptions UserAvatarImageOptions { get; set; }
//        public string[] EmailsBanList { get; set; }
//        public string[] PasswordsBanList { get; set; }
//        public void Validate()
//        {
//            Validator.ValidateObject(this, new ValidationContext(this), validateAllProperties: true);

//            if (string.IsNullOrEmpty(AdminUserSeed.Username))
//            {
//                throw new Exception("AdminUserSeed.Username must not be null or empty");
//            }
//            if (string.IsNullOrEmpty(AdminUserSeed.Password))
//            {
//                throw new Exception("AdminUserSeed.Password must not be null or empty");
//            }
//            if (string.IsNullOrEmpty(AdminUserSeed.Email))
//            {
//                throw new Exception("AdminUserSeed.Email must not be null or empty");
//            }
//            if (string.IsNullOrEmpty(AdminUserSeed.RoleName))
//            {
//                throw new Exception("AdminUserSeed.RoleName must not be null or empty");
//            }
//        }
//    }
//}
