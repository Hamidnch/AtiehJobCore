using AtiehJobCore.Core.Domain.Users;
using System.Collections.Generic;

namespace AtiehJobCore.Services.Users
{
    /// <summary>
    /// User attribute service
    /// </summary>
    public partial interface IUserAttributeService
    {
        /// <summary>
        /// Deletes a user attribute
        /// </summary>
        /// <param name="userAttribute">User attribute</param>
        void DeleteUserAttribute(UserAttribute userAttribute);

        /// <summary>
        /// Gets all user attributes
        /// </summary>
        /// <returns>User attributes</returns>
        IList<UserAttribute> GetAllUserAttributes();

        /// <summary>
        /// Gets a user attribute 
        /// </summary>
        /// <param name="userAttributeId">User attribute identifier</param>
        /// <returns>User attribute</returns>
        UserAttribute GetUserAttributeById(string userAttributeId);

        /// <summary>
        /// Inserts a user attribute
        /// </summary>
        /// <param name="userAttribute">User attribute</param>
        void InsertUserAttribute(UserAttribute userAttribute);

        /// <summary>
        /// Updates the user attribute
        /// </summary>
        /// <param name="userAttribute">User attribute</param>
        void UpdateUserAttribute(UserAttribute userAttribute);

        /// <summary>
        /// Deletes a user attribute value
        /// </summary>
        /// <param name="userAttributeValue">User attribute value</param>
        void DeleteUserAttributeValue(UserAttributeValue userAttributeValue);

        /// <summary>
        /// Inserts a user attribute value
        /// </summary>
        /// <param name="userAttributeValue">User attribute value</param>
        void InsertUserAttributeValue(UserAttributeValue userAttributeValue);

        /// <summary>
        /// Updates the user attribute value
        /// </summary>
        /// <param name="userAttributeValue">User attribute value</param>
        void UpdateUserAttributeValue(UserAttributeValue userAttributeValue);
    }
}
