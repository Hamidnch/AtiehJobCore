using AtiehJobCore.Common.MongoDb.Domain.Jobseekers;

namespace AtiehJobCore.Common.MongoDb.Domain.JobOpportunities
{
    /// <summary>
    /// رزرو یک فرصت شغلی توسط کارجو
    /// </summary>
    public class JobOpportunityReservation : BaseMongoEntity
    {
        /// <summary>
        /// کد کارجو
        /// </summary>
        public virtual int JobseekerCode { get; set; }
        public virtual Jobseeker Jobseeker { get; set; }

        /// <summary>
        /// کد فرصت شغلی
        /// </summary>
        public virtual int JobOpportunityCode { get; set; }
        public virtual JobOpportunity JobOpportunity { get; set; }
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
