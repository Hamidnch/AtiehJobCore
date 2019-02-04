using System;
using System.Collections.Generic;
using System.Text;

namespace AtiehJobCore.Common.MongoDb.Domain.Users
{
    /// <summary>
    /// User logged-in event
    /// </summary>
    public class UserLoggedInEvent
    {
        public UserLoggedInEvent(User user)
        {
            this.User = user;
        }

        /// <summary>
        /// User
        /// </summary>
        public User User
        {
            get; private set;
        }
    }

    /// <summary>
    /// User registered event
    /// </summary>
    public class UserRegisteredEvent
    {
        public UserRegisteredEvent(User user)
        {
            this.User = user;
        }

        /// <summary>
        /// User
        /// </summary>
        public User User
        {
            get; private set;
        }
    }
}
