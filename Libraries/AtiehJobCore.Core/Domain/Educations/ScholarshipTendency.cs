using System.Collections.Generic;
using AtiehJobCore.Core.Domain.JobOpportunities;
using AtiehJobCore.Core.Domain.Jobseekers;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Educations
{
    /// <summary>
    /// گرایش تحصیلی
    /// </summary>
    public class ScholarshipTendency : BaseMongoEntity
    {
        public ScholarshipTendency() { }

        public ScholarshipTendency(string title, int? scholarshipDisciplineCode)
        {
            Title = title;
            ScholarshipDisciplineCode = scholarshipDisciplineCode;
        }
        public string Title { get; set; }
        /// <summary>
        /// کد رشته تحصیلی
        /// </summary>
        public int? ScholarshipDisciplineCode { get; set; }

        public virtual ScholarshipDiscipline ScholarshipDiscipline { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        /// <summary>
        /// ارتباط با کلاس رشته های تحصیلی مورد نیاز فرصت شغلی
        /// </summary>
        public virtual ICollection<JobOpportunityEducation> JobOpportunityEducations { get; set; }
    }
}
