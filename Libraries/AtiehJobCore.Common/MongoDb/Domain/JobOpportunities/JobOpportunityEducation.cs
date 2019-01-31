using AtiehJobCore.Common.MongoDb.Domain.Educations;

namespace AtiehJobCore.Common.MongoDb.Domain.JobOpportunities
{
    /// <summary>
    /// رشته های تحصیلی مورد نیاز فرصت شغلی
    /// </summary>
    public class JobOpportunityEducation : BaseMongoEntity
    {
        public JobOpportunityEducation() { }

        public JobOpportunityEducation(int? educationLevelCode,
            int? scholarshipDisciplineCode, int? scholarshipTendencyCode,
            int? universityTypeCode, string universityName, int jobOpportunityCode)
        {
            EducationLevelCode = educationLevelCode;
            ScholarshipDisciplineCode = scholarshipDisciplineCode;
            ScholarshipTendencyCode = scholarshipTendencyCode;
            UniversityTypeCode = universityTypeCode;
            UniversityName = universityName;
            JobOpportunityCode = jobOpportunityCode;
        }
        /// <summary>
        /// مقطع تحصیلی
        /// </summary>
        public int? EducationLevelCode { get; set; }
        public virtual EducationLevel EducationLevel { get; set; }

        /// <summary>
        ///  رشته تحصیلی
        /// </summary>
        public int? ScholarshipDisciplineCode { get; set; }
        public virtual ScholarshipDiscipline ScholarshipDiscipline { get; set; }

        /// <summary>
        /// گرایش تحصیلی
        /// </summary>
        public int? ScholarshipTendencyCode { get; set; }
        public virtual ScholarshipTendency ScholarshipTendency { get; set; }
        /// <summary>
        /// نوع دانشگاه
        /// </summary>
        public int? UniversityTypeCode { get; set; }
        public virtual UniversityType UniversityType { get; set; }

        /// <summary>
        /// نام دانشگاه
        /// </summary>
        public string UniversityName { get; set; }
        /// <summary>
        /// ارتباط با کلاس فرصت شغلی
        /// Navigation Property
        /// </summary>
        public int JobOpportunityCode { get; set; }

        public virtual JobOpportunity JobOpportunity { get; set; }
    }
}
