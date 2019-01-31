using System.Collections.Generic;

namespace AtiehJobCore.Common.MongoDb.Domain.Jobseekers
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
