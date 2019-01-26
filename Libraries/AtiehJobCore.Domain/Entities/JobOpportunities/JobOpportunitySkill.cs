using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.Jobseekers;

namespace AtiehJobCore.Domain.Entities.JobOpportunities
{
    /// <summary>
    /// کلاس مهارتهای مورد نیاز فرصت شغلی
    /// </summary>
    public class JobOpportunitySkill : BaseEntity
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
