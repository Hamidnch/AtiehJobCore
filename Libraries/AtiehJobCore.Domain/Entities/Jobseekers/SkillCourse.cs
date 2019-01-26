using System.Collections.Generic;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.JobOpportunities;

namespace AtiehJobCore.Domain.Entities.Jobseekers
{
    /// <summary>
    /// دوره مهارتی
    /// </summary>
    public class SkillCourse : BaseEntity, IAuditableEntity
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
