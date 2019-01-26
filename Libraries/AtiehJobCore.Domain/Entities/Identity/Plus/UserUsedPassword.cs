using AtiehJobCore.Common.Contracts;

namespace AtiehJobCore.Domain.Entities.Identity.Plus
{
    public class UserUsedPassword : IAuditableEntity
    {
        public int Id { get; set; }
        public string HashedPassword { get; set; }

        public int UserId { get; set; }

        /// <summary>
        /// Navigation Properties
        /// </summary>
        public virtual User User { get; set; }
    }
}