using System.Collections.Generic;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.JobOpportunities;
using AtiehJobCore.Domain.Entities.Jobseekers;

namespace AtiehJobCore.Domain.Entities.Educations
{
    /// <summary>
    /// سطح تحصیلی
    /// </summary>
    public class EducationLevel : BaseEntity, IAuditableEntity
    {
        public EducationLevel() { }

        public EducationLevel(string title)
        {
            Title = title;
        }

        public string Title { get; set; }

        public virtual ICollection<Education> Educations { get; set; }
        /// <summary>
        /// ارتباط با کلاس رشته های تحصیلی مورد نیاز فرصت شغلی
        /// </summary>
        public virtual ICollection<JobOpportunityEducation> JobOpportunityEducations { get; set; }
    }
}
