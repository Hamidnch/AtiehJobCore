using AtiehJobCore.Core.Domain.Address;
using AtiehJobCore.Core.Domain.JobOpportunities;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.MongoDb;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtiehJobCore.Core.Domain.Jobseekers
{
    public class Jobseeker : BaseMongoEntity
    {
        #region Property
        /// <summary>
        /// شماره پرونده
        /// </summary>
        public string FileNumber { get; set; }
        /// <summary>
        /// نام کارجو
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string Family { get; set; }
        /// <summary>
        /// نام پدر
        /// </summary>
        public string FatherName { get; set; }

        /// <summary>
        /// محل تولد
        /// </summary>
        public string BirthPlace { get; set; }
        /// <summary>
        /// تلفن همراه
        /// </summary>
        public string MobileNumber { get; set; }
        public string NationalCode { get; set; }
        public string SsnId { get; set; }

        public string SsnSerial { get; set; }
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// تاریخ ثبت نام
        /// </summary>
        public DateTime? EnrollDate { get; set; }
        /// <summary>
        /// ساعت ثبت نام
        /// </summary>
        public string EnrollTime { get; set; }

        /// <summary>
        /// وضعیت تاهل
        /// </summary>
        public byte? Marital { get; set; }

        /// <summary>
        /// تعداد فرزندان
        /// </summary>
        public byte? ChildCount { get; set; }

        /// <summary>
        /// سرپرست خانوار
        /// </summary>
        public bool? HeadHousehold { get; set; }

        /// <summary>
        /// تعداد افراد تحت تکفل
        /// </summary>
        public byte? DependentPersonsCount { get; set; }

        /// <summary>
        /// دین
        /// </summary>
        public byte? ReligionId { get; set; }

        /// <summary>
        /// مذهب
        /// </summary>
        public string ReligionName { get; set; }

        /// <summary>
        /// تابعیت
        /// </summary>
        public byte? Nationality { get; set; }

        /// <summary>
        /// تحت پوشش
        /// </summary>
        public byte? CoveredBy { get; set; }

        /// <summary>
        /// وضعیت سربازی
        /// </summary>
        public byte? SoldierState { get; set; }

        /// <summary>
        /// علت معافیت
        /// </summary>
        public string ExemptionCase { get; set; }

        /// <summary>
        /// نوع پوشش لباس
        /// </summary>
        public byte? CoverageType { get; set; }

        /// <summary>
        ///قد برحسب سانتی متر
        /// </summary>
        public byte? Stature { get; set; }

        /// <summary>
        /// وزن برحسب کیلو
        /// </summary>
        public byte? Weight { get; set; }

        /// <summary>
        /// وضعیت فعالیت فعلی
        /// </summary>
        public byte? CurrentActivityStatus { get; set; }

        /// <summary>
        /// وشعیت قبل از جستجو
        /// </summary>
        public byte? BeforeSearchState { get; set; }

        /// <summary>
        /// محل بازنشستگی
        /// </summary>
        public string RetirementPlace { get; set; }

        /// <summary>
        /// دارای نامه نهادی
        /// </summary>
        public bool? IsInstitutionalLetter { get; set; }

        /// <summary>
        /// نامه نهادی از
        /// </summary>
        public int? InstitutionalLetterCode { get; set; }

        public virtual InstitutionalLetter InstitutionalLetter { get; set; }

        public byte[] Timestamp { get; set; }

        /**************** آدرس و تماس و شماره تلفن های ضروری *****************/
        /// <summary>
        /// کشور
        /// </summary>
        public int? CountryCode { get; set; }

        public virtual Country Country { get; set; }

        /// <summary>
        /// استان
        /// </summary>
        public int? ProvinceCode { get; set; }

        public virtual Province Province { get; set; }

        /// <summary>
        /// شهرستان
        /// </summary>
        public int? ShahrestanCode { get; set; }

        public virtual Shahrestan Shahrestan { get; set; }

        /// <summary>
        /// بخش
        /// </summary>
        public int? SectionCode { get; set; }

        public virtual Section Section { get; set; }

        /// <summary>
        /// شهر
        /// </summary>
        public int? CityCode { get; set; }

        public virtual City City { get; set; }

        /// <summary>
        /// خیابان
        /// </summary>
        public int? StreetCode { get; set; }
        public virtual Street Street { get; set; }

        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// تلفن تماس
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// کد پستی
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// تصویر آواتار
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// تلفن های ضروری
        /// </summary>
        public virtual ICollection<EssentialPhone> EssentialPhones { get; set; }

        /************************** وضعیت جسمانی و بیمه***************************/
        /// <summary>
        /// وضعیت ایثارگری
        /// </summary>
        public byte? SacrificeState { get; set; }

        /// <summary>
        /// وضعیت جسمانی
        /// </summary>
        public byte? PhysicalCondition { get; set; }

        /// <summary>
        /// نوع معلولیت
        /// </summary>
        public byte? DisabledType { get; set; }

        /// <summary>
        /// درصد معلولیت
        /// </summary>
        public byte? DisabledPercent { get; set; }

        /// <summary>
        /// وضعیت سابقه بیمه
        /// </summary>
        public bool? InsuranceHistoryState { get; set; }

        /// <summary>
        /// سازمان بیمه گر
        /// </summary>
        public byte? InsuranceOrganization { get; set; }

        /// <summary>
        /// شماره بیمه
        /// </summary>
        public string InsuranceNumber { get; set; }

        /// <summary>
        ///  سابقه بیمه برحسب ماه
        /// </summary>
        public byte? InsuranceHistory { get; set; }

        /// <summary>
        /// مشمول بیمه بیکاری
        /// </summary>
        public bool? UnemploymentInsurance { get; set; }

        /// <summary>
        /// تاریخ شروع مستمری
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? StartDatePension { get; set; }

        /// <summary>
        /// تاریخ پایان مستمری
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? EndDatePension { get; set; }
        /// <summary>
        /// وضعیت جاری
        /// </summary>
        public JobseekerState CurrentState { get; set; }
        /// <summary>
        /// نام کاریابی
        /// </summary>
        public string PlacementName { get; set; }


        /********************* بیماری های خاص ************************/
        /// <summary>
        /// آیا بیماری خاص دارد
        /// </summary>
        public bool? IsSpecialDisease { get; set; }

        /// <summary>
        /// بیماری های خاص
        /// </summary>
        public virtual ICollection<SpecialDisease> SpecialDiseases { get; set; }


        /********************* اخذ سوء پیشینه ***********************/
        /// <summary>
        /// آیا اخذ سوء پیشینه و یا عدم اعتیاد برای شما مقدور است؟
        /// </summary>
        public bool? CanHistoryAbuse { get; set; }

        /// <summary>
        /// درصورت عدم اخذ سوء پیشینه به چه دلیل
        /// </summary>
        public string HistoryAbuseDescription { get; set; }

        /********************* تحصیلات *******************************/
        /// <summary>
        /// لیستی از تحصیلات
        /// </summary>
        public virtual ICollection<Education> Educations { get; set; }


        /********************* زبان خارجه ***************************/
        /// <summary>
        /// زبان های خارجه
        /// </summary>
        public virtual ICollection<ForeignLanguage> ForeignLanguages { get; set; }

        /********************* مهارت ها *****************************/
        /// <summary>
        /// مهارت ها
        /// </summary>
        public virtual ICollection<Skill> Skills { get; set; }
        /********************* مهارت های درخواستی ******************/

        /// <summary>
        /// مهارت های درخواستی
        /// </summary>
        public virtual ICollection<SkillDemand> SkillsDemands { get; set; }

        /********************* وسیله نقلیه **************************/
        /// <summary>
        ///وسیله نقلیه
        /// </summary>
        public virtual ICollection<Vehicle> Vehicles { get; set; }

        /********************* گواهینامه ****************************/
        /// <summary>
        ///گواهینامه
        /// </summary>
        public virtual ICollection<DrivingLicense> DrivingLicenses { get; set; }

        /********************* سوابق شغلی ***************************/
        public virtual ICollection<OccupationalHistory> OccupationalHistories { get; set; }

        /************ کارفرمایانی که کارجو تمایل به همکاری با آنها را ندارد *************/
        /// <summary>
        /// عدم تمایل همکاری با کارفرمایان
        /// </summary>
        public virtual ICollection<DontWantEmployer> DontWantEmployers { get; set; }
        /********************* مشاغل درخواستی **********************/

        /// <summary>
        /// مشاغل درخواستی
        /// </summary>
        public virtual ICollection<JobDemand> JobDemands { get; set; }

        /// <summary>
        /// نوع زمان کاری
        /// </summary>
        public string WorkTime { get; set; }

        /// <summary>
        /// نوع دستمزد
        /// </summary>
        public WageTypes? WageType { get; set; }

        /// <summary>
        /// حداقل دستمزد
        /// </summary>
        public long? WageMinimum { get; set; }

        /// <summary>
        /// ارتباط با کلاس رزرو شغل
        /// </summary>
        public virtual ICollection<JobOpportunityReservation> JobOpportunityReservations { get; set; }

        /***************** ارتباط با جدول کاربر ********************/
        /// <summary>
        /// شناسه کاربری
        /// </summary>
        public string UserId { get; set; }
        public virtual User User { get; set; }
        /***************** ارتباط با جدول کاربر ********************/
        #endregion Property
    }
}
