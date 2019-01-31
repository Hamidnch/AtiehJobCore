namespace AtiehJobCore.Common.MongoDb.Domain.Jobseekers
{
    /// <summary>
    /// مهارت های درخواستی
    /// </summary>
    public class SkillDemand : BaseMongoEntity
    {
        public SkillDemand() { }

        public SkillDemand(int? skillCourseCode, int jobseekerId)
        {
            SkillCourseCode = skillCourseCode;
            JobseekerId = jobseekerId;
        }
        /// <summary>
        /// کد دوره مهارتی
        /// </summary>
        public int? SkillCourseCode { get; set; }
        public virtual SkillCourse SkillCourse { get; set; }
        /// <summary>
        /// کد کارجو
        /// </summary>
        public int JobseekerId { get; set; }

        /// <summary>
        /// ارتباط با کلاس کارجو
        /// Navigation Property
        /// </summary>
        public virtual Jobseeker Jobseeker { get; set; }
    }
}
