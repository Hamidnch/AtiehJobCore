using System;
using System.Collections.Generic;
using System.Text;

namespace AtiehJobCore.Common.MongoDb.Domain.Security
{
    /// <summary>
    /// Represents a permission record
    /// </summary>
    public partial class PermissionRecord : BaseMongoEntity
    {
        private ICollection<string> _userRoles;

        /// <summary>
        /// Gets or sets the permission name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the permission system name
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the permission category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        public virtual ICollection<string> UserRoles
        {
            get => _userRoles ?? (_userRoles = new List<string>());
            protected set => _userRoles = value;
        }
    }
}
