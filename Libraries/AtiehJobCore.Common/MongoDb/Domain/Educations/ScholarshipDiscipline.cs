using System.Collections.Generic;
using AtiehJobCore.Common.MongoDb.Domain.JobOpportunities;
using AtiehJobCore.Common.MongoDb.Domain.Jobseekers;

namespace AtiehJobCore.Common.MongoDb.Domain.Educations
{
    /// <summary>
    /// کلاس رشته های تحصیلی
    /// </summary>
    public class ScholarshipDiscipline : BaseMongoEntity
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
