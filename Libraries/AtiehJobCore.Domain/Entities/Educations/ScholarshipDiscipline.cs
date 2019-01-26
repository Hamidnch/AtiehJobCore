using System.Collections.Generic;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.JobOpportunities;
using AtiehJobCore.Domain.Entities.Jobseekers;

namespace AtiehJobCore.Domain.Entities.Educations
{
    /// <summary>
    /// کلاس رشته های تحصیلی
    /// </summary>
    public class ScholarshipDiscipline : BaseEntity, IAuditableEntity
    {
        public ScholarshipDiscipline() { }

        public ScholarshipDiscipline(string title)
        {
            Title = title;
        }
        /// <summary>
        /// عنوان رشته تحصیلی
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// ارتباط با گرایش تحصیلی 
        /// </summary>
        public virtual ICollection<ScholarshipTendency> ScholarshipTendencies { get; set; }
        /// <summary>
        /// ارتباط با تحصیلات کارجو
        /// </summary>
        public virtual ICollection<Education> Educations { get; set; }
        /// <summary>
        /// ارتباط با کلاس رشته های تحصیلی مورد نیاز فرصت شغلی
        /// </summary>
        public virtual ICollection<JobOpportunityEducation> JobOpportunityEducations { get; set; }
    }
}
