using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AtiehJobCore.Common.Enums;
using AtiehJobCore.Common.MongoDb.Domain.Employers;
using AtiehJobCore.Common.MongoDb.Domain.Occupations;

namespace AtiehJobCore.Common.MongoDb.Domain.JobOpportunities
{
    /// <summary>
    /// فرصت شغلی
    /// </summary>
    public class JobOpportunity : BaseMongoEntity
    {
        public JobOpportunity() { }

        [SuppressMessage("ReSharper", "FunctionComplexityOverflow")]
        public JobOpportunity(string identityNumber, int occupationalGroupCode,
            int occupationalTitleCode, int employerCode,
            //string persianEnrollDate, string persianRepeatDate, 
            DateTime? enrollDate, DateTime? repeatDate, GenderTypes2? genderTypes2,
            byte? manCount, byte? womanCount, WorkTimes? workTime, string startTime,
            string endTime, int? occupationalHistoryRequired, SoldierStates2? soldierState,
            MaritalStatus? marital, byte? startAge, byte? endAge, string applicant,
            string applicantPost, WageTypes? wageType, long? salaryFrom, long? salaryUntil,
            ExpertStatus? faceQuality, CoverageTypes? coverage, Dialects? dialect,
            ExpertStatus? publicRelations, ExpertStatus? physicalForces, ExpertStatus? generalInfoStatus,
            int? employerAddressCode, string phoneForInterview, bool? isPartTimeWork,
            bool? ladiesSuitable, bool? isInsurance, bool? retireesSuitable, string employerAddressList)
        {
            IdentityNumber = identityNumber;
            OccupationalGroupCode = occupationalGroupCode;
            OccupationalTitleCode = occupationalTitleCode;
            EmployerCode = employerCode;
            EnrollDate = enrollDate;
            //PersianEnrollDate = persianEnrollDate;
            RepeatDate = repeatDate;
            //PersianRepeatDate = persianRepeatDate;
            GenderTypes2 = genderTypes2;
            ManCount = manCount;
            WomanCount = womanCount;
            WorkTime = workTime;
            StartTime = startTime;
            EndTime = endTime;
            OccupationalHistoryRequired = occupationalHistoryRequired;
            SoldierState = soldierState;
            Marital = marital;
            StartAge = startAge;
            EndAge = endAge;
            Applicant = applicant;
            ApplicantPost = applicantPost;
            WageType = wageType;
            SalaryFrom = salaryFrom;
            SalaryUntil = salaryUntil;
            FaceQuality = faceQuality;
            Coverage = coverage;
            Dialect = dialect;
            PublicRelations = publicRelations;
            PhysicalForces = physicalForces;
            GeneralInfoStatus = generalInfoStatus;
            InterviewAddressCode = employerAddressCode;
            InterviewPhone = phoneForInterview;
            IsPartTimeWork = isPartTimeWork;
            LadiesSuitable = ladiesSuitable;
            IsInsurance = isInsurance;
            RetireesSuitable = retireesSuitable;
            EmployerAddressList = employerAddressList;
        }
        /// <summary>
        /// شناسه فرصت شغلی
        /// </summary>
        public string IdentityNumber { get; set; }
        /// <summary>
        /// کد گروه شغلی
        /// </summary>
        public int? OccupationalGroupCode { get; set; }
        public virtual OccupationalGroup OccupationalGroup { get; set; }

        /// <summary>
        /// کد عنوان شغل
        /// </summary>
        public int? OccupationalTitleCode { get; set; }
        public virtual OccupationalTitle OccupationalTitle { get; set; }

        /// <summary>
        /// کد کارفرما
        /// </summary>
        public int EmployerCode { get; set; }
        /// <summary>
        /// ارتباط با کارفرما
        /// </summary>
        public virtual Employer Employer { get; set; }

        /// <summary>
        /// تاریخ ثبت
        /// </summary>
        //[Column(TypeName = "date")]
        public DateTime? EnrollDate { get; set; }

        //public string PersianEnrollDate { get; set; }
        /// <summary>
        /// تاریخ تمدید
        /// </summary>
        //[Column(TypeName = "date")]
        public DateTime? RepeatDate { get; set; }
        //public string PersianRepeatDate { get; set; }

        /// <summary>
        /// جنسیت
        /// </summary>
        public GenderTypes2? GenderTypes2 { get; set; }
        /// <summary>
        /// تعداد نیروی مرد
        /// </summary>
        public byte? ManCount { get; set; }
        /// <summary>
        /// تعداد نیروی زن
        /// </summary>
        public byte? WomanCount { get; set; }
        /// <summary>
        /// اوقات کاری
        /// </summary>
        public WorkTimes? WorkTime { get; set; }
        /// <summary>
        /// ساعت کاری از
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// ساعت کاری تا
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// سابقه شغلی موردنیاز
        /// </summary>
        public int? OccupationalHistoryRequired { get; set; }
        /// <summary>
        /// وضعیت سربازی
        /// </summary>
        public SoldierStates2? SoldierState { get; set; }
        /// <summary>
        /// وضعیت تاهل
        /// </summary>
        public MaritalStatus? Marital { get; set; }
        /// <summary>
        /// سن از
        /// </summary>
        public byte? StartAge { get; set; }
        /// <summary>
        /// سن تا
        /// </summary>
        public byte? EndAge { get; set; }

        /// <summary>
        /// سفارش دهنده
        /// </summary>
        public string Applicant { get; set; }
        /// <summary>
        /// سمت سفارش دهنده
        /// </summary>
        public string ApplicantPost { get; set; } //ApplicantPosts
        /// <summary>
        /// نوع حقوق
        /// </summary>
        public WageTypes? WageType { get; set; }
        /// <summary>
        /// حقوق از
        /// </summary>
        public long? SalaryFrom { get; set; }
        /// <summary>
        /// حقوق تا
        /// </summary>
        public long? SalaryUntil { get; set; }
        /// <summary>
        /// کیفیت سیما
        /// </summary>
        public ExpertStatus? FaceQuality { get; set; }
        /// <summary>
        /// نوع پوشش
        /// </summary>
        public CoverageTypes? Coverage { get; set; }
        /// <summary>
        /// گویش
        /// </summary>
        public Dialects? Dialect { get; set; }
        /// <summary>
        /// روابط عمومی
        /// </summary>
        public ExpertStatus? PublicRelations { get; set; }
        /// <summary>
        /// قوای جسمانی
        /// </summary>
        public ExpertStatus? PhysicalForces { get; set; }
        /// <summary>
        /// وضعیت اطلاعات عمومی و شغلی
        /// </summary>
        public ExpertStatus? GeneralInfoStatus { get; set; }
        /// <summary>
        /// کد آدرس محل مصاحبه
        /// </summary>
        public int? InterviewAddressCode { get; set; }
        public virtual EmployerAddress InterviewEmployerAddress { get; set; }
        /// <summary>
        /// تلفن هماهنگی برای مصاحبه
        /// </summary>
        public string InterviewPhone { get; set; }
        /// <summary>
        /// امکان پاره وقتی کار
        /// </summary>
        public bool? IsPartTimeWork { get; set; }
        /// <summary>
        /// مناسب برای خانم ها
        /// </summary>
        public bool? LadiesSuitable { get; set; }
        /// <summary>
        /// بیمه
        /// </summary>
        public bool? IsInsurance { get; set; }
        /// <summary>
        /// مناسب برای بازنشستگان
        /// </summary>
        public bool? RetireesSuitable { get; set; }

        /// <summary>
        /// لیستی از کد آدرس های کارفرما
        /// </summary>
        public string EmployerAddressList { get; set; }

        /// <summary>
        /// وضعیت جاری
        /// </summary>
        public string CurrentState { get; set; }
        /// <summary>
        /// نام کاریابی
        /// </summary>
        public string PlacementName { get; set; }
        /// <summary>
        ///لیستی از رشته های تحصیلی مرتبط با فرصت شغلی
        /// </summary>
        public virtual ICollection<JobOpportunityEducation> JobOpportunityEducations { get; set; }
        /// <summary>
        /// ارتباط با کلاس مهارتهای موردنیاز فرصت شغلی
        /// </summary>
        public virtual ICollection<JobOpportunitySkill> JobOpportunitySkills { get; set; }

        /// <summary>
        /// ارتباط با کلاس رزرو شغل
        /// </summary>
        public virtual ICollection<JobOpportunityReservation> JobOpportunityReservations { get; set; }
    }
}
