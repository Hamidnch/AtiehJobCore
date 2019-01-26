using System.Collections.Generic;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;

namespace AtiehJobCore.Domain.Entities.Jobseekers
{
    /// <summary>
    /// نام گواهینامه رانندگی
    /// </summary>
    public class DrivingLicenseName : BaseEntity, IAuditableEntity
    {
        public DrivingLicenseName() { }

        public DrivingLicenseName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// نام گواهینامه
        /// </summary>
        public string Name { get; set; }
        public virtual ICollection<DrivingLicense> DrivingLicenses { get; set; }
    }
}
