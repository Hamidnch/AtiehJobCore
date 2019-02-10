using System.Collections.Generic;
using AtiehJobCore.Core.Domain.JobOpportunities;
using AtiehJobCore.Core.Domain.Jobseekers;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Educations
{
    /// <summary>
    /// انواع دانشگاه
    /// </summary>
    public class UniversityType : BaseMongoEntity
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
