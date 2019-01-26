using AtiehJobCore.Domain.Entities.Employers;

namespace AtiehJobCore.Domain.Entities.JobOpportunities
{
    /// <summary>
    /// جدول واسط بین فرصت شغلی و آدرس های کارفرما
    /// </summary>
    public class JobOpportunityEmployerAddress
    {
        /// <summary>
        /// کد فرصت شغلی
        /// </summary>
        public int JobOpportunityCode { get; set; }
        public JobOpportunity JobOpportunity { get; set; }
        /// <summary>
        /// کد آدرس کارفرما
        /// </summary>
        public int EmployerAddressCode { get; set; }
        public virtual EmployerAddress EmployerAddress { get; set; }
    }
}
