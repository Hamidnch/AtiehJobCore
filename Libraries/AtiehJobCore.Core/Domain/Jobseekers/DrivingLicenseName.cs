using System.Collections.Generic;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Jobseekers
{
    /// <summary>
    /// نام گواهینامه رانندگی
    /// </summary>
    public class DrivingLicenseName : BaseMongoEntity
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
