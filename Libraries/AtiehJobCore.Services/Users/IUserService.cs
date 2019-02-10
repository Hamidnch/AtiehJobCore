using AtiehJobCore.Core;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Enums;
using System;
using System.Collections.Generic;

namespace AtiehJobCore.Services.Users
{
    public partial interface IUserService
    {
        #region Users

        /// <summary>
        /// Gets all Users
        /// </summary>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records</param>
        /// <param name="userRoleIds">A list of User role identifiers to filter by (at least one match); pass null or empty list in order to load all Users; </param>
        /// <param name="email">Email; null to load all Users</param>
        /// <param name="username">Username; null to load all Users</param>
        /// <param name="firstName">First name; null to load all Users</param>
        /// <param name="lastName">Last name; null to load all Users</param>
        /// <param name="company">Company; null to load all Users</param>
        /// <param name="phone">Phone; null to load all Users</param>
        /// <param name="zipPostalCode">Phone; null to load all Users</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Users</returns>
        IPagedList<User> GetAllUsers(DateTime? createdFromUtc = null,
            DateTime? createdToUtc = null, string[] userRoleIds = null,
            string email = null, string username = null,
            string firstName = null, string lastName = null,
            string company = null, string phone = null, string zipPostalCode = null,
            int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets all Users by User format (including deleted ones)
        /// </summary>
        /// <param name="passwordFormat">Password format</param>
        /// <returns>Users</returns>
        IList<User> GetAllUsersByPasswordFormat(PasswordFormat passwordFormat);

        /// <summary>
        /// Gets online Users
        /// </summary>
        /// <param name="lastActivityFromUtc">User last activity date (from)</param>
        /// <param name="userRoleIds">A list of User role identifiers to filter by (at least one match); pass null or empty list in order to load all Users; </param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Users</returns>
        IPagedList<User> GetOnlineUsers(DateTime lastActivityFromUtc,
            string[] userRoleIds, int pageIndex = 0, int pageSize = int.MaxValue);


        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="user">User</param>
        void DeleteUser(User user);

        /// <summary>
        /// Gets a User
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>A User</returns>
        User GetUserById(string userId);

        /// <summary>
        /// Get Users by identifiers
        /// </summary>
        /// <param name="userIds">User identifiers</param>
        /// <returns>Users</returns>
        IList<User> GetUsersByIds(string[] userIds);

        /// <summary>
        /// Gets a User by GUID
        /// </summary>
        /// <param name="userGuid">User GUID</param>
        /// <returns>A User</returns>
        User GetUserByGuid(Guid userGuid);

        /// <summary>
        /// Get User by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>User</returns>
        User GetUserByEmail(string email);

        /// <summary>
        /// Get User by system role
        /// </summary>
        /// <param name="systemName">System name</param>
        /// <returns>User</returns>
        User GetUserBySystemName(string systemName);

        /// <summary>
        /// Get User by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>User</returns>
        User GetUserByUsername(string username);
        /// <summary>
        /// Get User by mobile number
        /// </summary>
        /// <param name="mobileNumber">Username</param>
        /// <returns>User</returns>
        User GetUserByMobileNumber(string mobileNumber);
        /// <summary>
        /// Get User by national code
        /// </summary>
        /// <param name="nationalCode">Username</param>
        /// <returns>User</returns>
        User GetUserByNationalCode(string nationalCode);

        /// <summary>
        /// Insert a guest User
        /// </summary>
        /// <returns>User</returns>
        User InsertGuestUser(string urlReferrer = "");

        /// <summary>
        /// Insert a User
        /// </summary>
        /// <param name="user">User</param>
        void InsertUser(User user);

        /// <summary>
        /// Updates the User
        /// </summary>
        /// <param name="user">User</param>
        void UpdateUser(User user);

        /// <summary>
        /// Updates the User
        /// </summary>
        /// <param name="user">User</param>
        void UpdateUserLastActivityDate(User user);

        /// <summary>
        /// Updates the User
        /// </summary>
        /// <param name="user">User</param>
        void UpdateUserPassword(User user);

        /// <summary>
        /// Updates the User
        /// </summary>
        /// <param name="user">User</param>
        void UpdateActive(User user);

        /// <summary>
        /// Updates the User
        /// </summary>
        /// <param name="user">User</param>
        void UpdateUserLastLoginDate(User user);

        /// <summary>
        /// Updates the User
        /// </summary>
        /// <param name="user">User</param>
        void UpdateUserLastIpAddress(User user);

        /// <summary>
        /// Updates the User in admin panel
        /// </summary>
        /// <param name="user">User</param>
        void UpdateUserInAdminPanel(User user);

        /// <summary>
        /// Delete guest User records
        /// </summary>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records</param>
        /// <returns>Number of deleted Users</returns>
        int DeleteGuestUsers(DateTime? createdFromUtc, DateTime? createdToUtc);

        #endregion

        #region Password history

        /// <summary>
        /// Gets User passwords
        /// </summary>
        /// <param name="userId">User identifier; pass null to load all records</param>
        /// <param name="passwordsToReturn">Number of returning passwords; pass null to load all records</param>
        /// <returns>List of User passwords</returns>
        IList<UserHistoryPassword> GetPasswords(string userId, int passwordsToReturn);

        /// <summary>
        /// Insert a User history password
        /// </summary>
        /// <param name="user">User</param>
        void InsertUserPassword(User user);


        #endregion

        #region User roles

        /// <summary>
        /// Inserts a User role
        /// </summary>
        /// <param name="userRole">User role</param>
        void InsertUserRole(Role userRole);

        /// <summary>
        /// Updates the User role
        /// </summary>
        /// <param name="userRole">User role</param>
        void UpdateUserRole(Role userRole);

        /// <summary>
        /// Delete a User role
        /// </summary>
        /// <param name="userRole">User role</param>
        void DeleteUserRole(Role userRole);

        /// <summary>
        /// Gets a User role
        /// </summary>
        /// <param name="userRoleId">User role identifier</param>
        /// <returns>User role</returns>
        Role GetUserRoleById(string userRoleId);

        /// <summary>
        /// Gets a User role
        /// </summary>
        /// <param name="systemName">User role system name</param>
        /// <returns>User role</returns>
        Role GetUserRoleBySystemName(string systemName);

        /// <summary>
        /// Gets all User roles
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>User roles</returns>
        IList<Role> GetAllUserRoles(bool showHidden = false);

        #endregion

        #region User Role in User

        void InsertUserRoleInUser(Role userRole);

        void DeleteUserRoleInUser(Role userRole);

        #endregion
    }
}
