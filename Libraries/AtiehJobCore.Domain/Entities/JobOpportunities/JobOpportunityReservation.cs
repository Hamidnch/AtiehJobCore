using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.Jobseekers;

namespace AtiehJobCore.Domain.Entities.JobOpportunities
{
    /// <summary>
    /// رزرو یک فرصت شغلی توسط کارجو
    /// </summary>
    public class JobOpportunityReservation : BaseEntity
    {
        public virtual Jobseeker Jobseeker { get; set; }

        /// <summary>
        /// کد کارجو
        /// </summary>
        public virtual int JobseekerCode { get; set; }

        public virtual JobOpportunity JobOpportunity { get; set; }

        /// <summary>
        /// کد فرصت شغلی
        /// </summary>
        public virtual int JobOpportunityCode { get; set; }

        /// <summary>
        /// وضعیت رزرو
        /// </summary>
        public virtual string State { get; set; }

        /// <summary>
        /// اولویت
        /// </summary>
        public virtual int Priority { get; set; }
    }
}