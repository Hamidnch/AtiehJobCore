using System;
using AtiehJobCore.Core.Domain.Address;
using AtiehJobCore.Core.Domain.Educations;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Jobseekers
{
    /// <summary>
    ///  تحصیلات کارجو
    /// </summary>
    public class Education : BaseMongoEntity
    {
        public Education() { }

        public Education(int? educationLevelCode, bool? isStudent, int? scholarshipDisciplineCode,
            int? scholarshipTendencyCode, int? educationProvinceCode, string otherProvinces,
            int? universityTypeCode, string universityName, DateTime? graduationDate,
            decimal? average, byte? passUnitCount, int jobSeekerId)
        {
            EducationLevelCode = educationLevelCode;
            IsStudent = isStudent;
            ScholarshipDisciplineCode = scholarshipDisciplineCode;
            ScholarshipTendencyCode = scholarshipTendencyCode;
            ProvinceCode = educationProvinceCode;
            OtherProvinces = otherProvinces;
            UniversityTypeCode = universityTypeCode;
            UniversityName = universityName;
            GraduationDate = graduationDate;
            Average = average;
            PassUnitCount = passUnitCount;
            JobseekerCode = jobSeekerId;
        }

        /// <summary>
        /// مقطع تحصیلی
        /// </summary>
        public int? EducationLevelCode { get; set; }
        public virtual EducationLevel EducationLevel { get; set; }

        /// <summary>
        /// مشغول به تحصیل
        /// </summary>
        public bool? IsStudent { get; set; }

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
        /// استان محل تحصیل
        /// </summary>
        public int? ProvinceCode { get; set; }
        public virtual Province Province { get; set; }

        /// <summary>
        /// سایر استان ها
        /// </summary>
        public string OtherProvinces { get; set; }

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
        /// تاریخ اخذ مدرک
        /// </summary>
        public DateTime? GraduationDate { get; set; }

        /// <summary>
        /// معدل
        /// </summary>
        public decimal? Average { get; set; }

        /// <summary>
        /// تعداد واحد گذرانده
        /// </summary>
        public byte? PassUnitCount { get; set; }

        /// <summary>
        /// کد کارجو
        /// </summary>
        public int JobseekerCode { get; set; }

        /// <summary>
        /// ارتباط با کلاس کارجو
        /// Navigation Property
        /// </summary>
        public virtual Jobseeker Jobseeker { get; set; }
    }
}
