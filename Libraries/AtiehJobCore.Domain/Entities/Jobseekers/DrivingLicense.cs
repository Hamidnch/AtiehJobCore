using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;

namespace AtiehJobCore.Domain.Entities.Jobseekers
{
    /// <summary>
    /// گواهینامه رانندگی
    /// </summary>
    public class DrivingLicense : BaseEntity, IAuditableEntity
    {
        public DrivingLicense() { }

        public DrivingLicense(int? drivingLicenseNameCode, int jobseekerId)
        {
            DrivingLicenseNameCode = drivingLicenseNameCode;
            JobseekerId = jobseekerId;
        }
        /// <summary>
        /// کد نام گواهینامه
        /// </summary>
        public int? DrivingLicenseNameCode { get; set; }
        public virtual DrivingLicenseName DrivingLicenseName { get; set; }
        /// <summary>
        /// کد کارجو
        /// </summary>
        public int JobseekerId { get; set; }

        /// <summary>
        /// ارتباط با کلاس کارجو
        /// Navigation Property
        /// </summary>
        public virtual Jobseeker Jobseeker { get; set; }
    }
}
