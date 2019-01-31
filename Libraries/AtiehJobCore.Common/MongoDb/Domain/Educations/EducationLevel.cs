using System.Collections.Generic;
using AtiehJobCore.Common.MongoDb.Domain.JobOpportunities;
using AtiehJobCore.Common.MongoDb.Domain.Jobseekers;

namespace AtiehJobCore.Common.MongoDb.Domain.Educations
{
    /// <summary>
    /// سطح تحصیلی
    /// </summary>
    public class EducationLevel : BaseMongoEntity
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
