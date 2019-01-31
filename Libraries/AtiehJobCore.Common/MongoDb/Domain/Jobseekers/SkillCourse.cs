using System.Collections.Generic;
using AtiehJobCore.Common.MongoDb.Domain.JobOpportunities;

namespace AtiehJobCore.Common.MongoDb.Domain.Jobseekers
{
    /// <summary>
    /// دوره مهارتی
    /// </summary>
    public class SkillCourse : BaseMongoEntity
    {
        public SkillCourse() { }

        public SkillCourse(string name)
        {
            Name = name;
        }
        /// <summary>
        /// نام دوره
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ارتباط با کلاس مهارتهای درخواستی
        /// </summary>
        public virtual ICollection<SkillDemand> SkillDemands { get; set; }
        /// <summary>
        /// ارتباط با کلاس مهارتهای موردنیاز فرصت شغلی
        /// </summary>
        public virtual ICollection<JobOpportunitySkill> JobOpportunitySkills { get; set; }
    }
}
