using AtiehJobCore.Common.MongoDb.Domain.Jobseekers;

namespace AtiehJobCore.Common.MongoDb.Domain.JobOpportunities
{
    /// <summary>
    /// کلاس مهارتهای مورد نیاز فرصت شغلی
    /// </summary>
    public class JobOpportunitySkill : BaseMongoEntity
    {
        public JobOpportunitySkill() { }

        public JobOpportunitySkill(int? skillCourseCode, int jobOpportunityCode, string description)
        {
            SkillCourseCode = skillCourseCode;
            JobOpportunityCode = jobOpportunityCode;
            Description = description;
        }
        /// <summary>
        /// کد دوره مهارتی
        /// </summary>
        public int? SkillCourseCode { get; set; }
        public virtual SkillCourse SkillCourse { get; set; }
        /// <summary>
        /// کد فرصت شغلی
        /// </summary>
        public int JobOpportunityCode { get; set; }

        /// <summary>
        /// ارتباط با کلاس کارجو
        /// Navigation Property
        /// </summary>
        public virtual JobOpportunity JobOpportunity { get; set; }
    }
}
