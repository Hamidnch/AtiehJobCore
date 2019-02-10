using System;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Jobseekers
{
    /// <summary>
    ///  مهارت های کارجو
    /// </summary>
    public class Skill : BaseMongoEntity
    {
        public Skill() { }

        public Skill(string name, SkillDegrees? skillDegree, LearningMethods? learningMethod,
            byte? periodTime, string collegeName, DateTime? endPeriodDate,
            CollegeTypes? collegeType, int jobSeekerId)
        {
            Name = name;
            SkillDegree = skillDegree;
            LearningMethod = learningMethod;
            PeriodTime = periodTime;
            CollegeName = collegeName;
            EndPeriodDate = endPeriodDate;
            CollegeType = collegeType;
            JobseekerId = jobSeekerId;
        }
        /// <summary>
        /// نام دوره
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// درجه مهارت
        /// </summary>
        public SkillDegrees? SkillDegree { get; set; }

        /// <summary>
        /// روش فراگیری
        /// </summary>
        public LearningMethods? LearningMethod { get; set; }

        /// <summary>
        /// مدت دوره
        /// </summary>
        public byte? PeriodTime { get; set; }

        /// <summary>
        /// نام آموزشگاه
        /// </summary>
        public string CollegeName { get; set; }

        /// <summary>
        /// تاریخ پایان دوره
        /// </summary>
        public DateTime? EndPeriodDate { get; set; }

        /// <summary>
        /// نوع آموزشگاه
        /// </summary>
        public CollegeTypes? CollegeType { get; set; }
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
