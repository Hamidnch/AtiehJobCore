using System.Collections.Generic;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.JobOpportunities;
using AtiehJobCore.Domain.Entities.Jobseekers;

namespace AtiehJobCore.Domain.Entities.Educations
{
    /// <summary>
    /// انواع دانشگاه
    /// </summary>
    public class UniversityType : BaseEntity, IAuditableEntity
    {
        public UniversityType() { }

        public UniversityType(string type)
        {
            Type = type;
        }

        /// <summary>
        /// نوع دانشگاه
        /// </summary>
        public string Type { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        /// <summary>
        /// ارتباط با کلاس رشته های تحصیلی مورد نیاز فرصت شغلی
        /// </summary>
        public virtual ICollection<JobOpportunityEducation> JobOpportunityEducations { get; set; }
    }
}
