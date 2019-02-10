using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Security
{
    /// <summary>
    /// Represents an ACL record
    /// </summary>
    public partial class AclRecord : BaseMongoEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// Gets or sets the entity name
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Gets or sets the user role identifier
        /// </summary>
        public string UserRoleId { get; set; }

        /// <summary>
        /// Gets or sets the user role
        /// </summary>
        public virtual Role UserRole { get; set; }
    }
}
