using AtiehJobCore.Core.Enums;

namespace AtiehJobCore.Core.Domain.Users
{
    /// <summary>
    /// User registration request
    /// </summary>
    public class UserRegistrationRequest
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="email">Email</param>
        /// <param name="username">Username</param>
        /// <param name="nationalCode"></param>
        /// <param name="password">Password</param>
        /// <param name="passwordFormat">Password format</param>
        /// <param name="userType"></param>
        /// <param name="isApproved">Is approved</param>
        /// <param name="mobileNumber"></param>
        public UserRegistrationRequest(User user, string email, string username,
            string mobileNumber, string nationalCode,
            string password, PasswordFormat passwordFormat, UserType userType, bool isApproved = true)
        {
            User = user;
            Email = email;
            Username = username;
            MobileNumber = mobileNumber;
            NationalCode = nationalCode;
            Password = password;
            PasswordFormat = passwordFormat;
            UserType = userType;
            IsApproved = isApproved;
        }

        /// <summary>
        /// User
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Password format
        /// </summary>
        public PasswordFormat PasswordFormat { get; set; }
        /// <summary>
        /// MobileNumber
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// National Code
        /// </summary>
        public string NationalCode { get; set; }
        /// <summary>
        /// Is approved
        /// </summary>
        public bool IsApproved { get; set; }

        public UserType UserType { get; set; }
    }
}
