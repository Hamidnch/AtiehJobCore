using AtiehJobCore.Core.Domain.Address;
using AtiehJobCore.Core.Domain.Jobseekers;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Web.Framework.Models.Newsletter;
using AtiehJobCore.Web.Framework.Models.User;
using AtiehJobCore.Web.Framework.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Web.Framework.Models.Account.Jobseeker
{
    public partial class RegisterJobseekerModel : BaseMongoModel
    {
        public RegisterJobseekerModel()
        {
            this.AvailableTimeZones = new List<SelectListItem>();
            this.AvailableCountries = new List<SelectListItem>();
            this.AvailableStates = new List<SelectListItem>();
            this.UserAttributes = new List<UserAttributeModel>();
            this.NewsletterCategories = new List<NewsletterSimpleCategory>();
        }

        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Email")]
        public string Email { get; set; }

        public bool UsernamesEnabled { get; set; }
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Username")]
        public string Username { get; set; }

        public bool CheckUsernameAvailabilityEnabled { get; set; }

        [DataType(DataType.Password)]
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        //form fields & properties
        public bool GenderEnabled { get; set; }
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Gender")]
        public GenderType Gender { get; set; }

        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.FirstName")]
        public string FirstName { get; set; }

        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.LastName")]
        public string LastName { get; set; }


        public bool DateOfBirthEnabled { get; set; }
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.DateOfBirth")]
        public int? DateOfBirthDay { get; set; }
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.DateOfBirth")]
        public int? DateOfBirthMonth { get; set; }
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.DateOfBirth")]
        public int? DateOfBirthYear { get; set; }
        public bool DateOfBirthRequired { get; set; }
        public DateTime? ParseDateOfBirth()
        {
            if (!DateOfBirthYear.HasValue || !DateOfBirthMonth.HasValue || !DateOfBirthDay.HasValue)
                return null;

            DateTime? dateOfBirth = null;
            try
            {
                dateOfBirth = new DateTime(DateOfBirthYear.Value, DateOfBirthMonth.Value, DateOfBirthDay.Value);
            }
            catch
            {
                // ignored
            }

            return dateOfBirth;
        }

        public bool CompanyEnabled { get; set; }
        public bool CompanyRequired { get; set; }
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Company")]
        public string Company { get; set; }

        public bool StreetAddressEnabled { get; set; }
        public bool StreetAddressRequired { get; set; }
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.StreetAddress")]
        public string StreetAddress { get; set; }

        public bool StreetAddress2Enabled { get; set; }
        public bool StreetAddress2Required { get; set; }
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.StreetAddress2")]
        public string StreetAddress2 { get; set; }

        public bool ZipPostalCodeEnabled { get; set; }
        public bool ZipPostalCodeRequired { get; set; }
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.ZipPostalCode")]
        public string ZipPostalCode { get; set; }

        public bool CityEnabled { get; set; }
        public bool CityRequired { get; set; }
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.City")]
        public string City { get; set; }

        public bool CountryEnabled { get; set; }
        public bool CountryRequired { get; set; }
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Country")]
        public string CountryId { get; set; }
        public IList<SelectListItem> AvailableCountries { get; set; }

        public bool StateProvinceEnabled { get; set; }
        public bool StateProvinceRequired { get; set; }
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.StateProvince")]
        public string StateProvinceId { get; set; }
        public IList<SelectListItem> AvailableStates { get; set; }

        public bool PhoneEnabled { get; set; }
        public bool PhoneRequired { get; set; }
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Phone")]
        public string Phone { get; set; }

        public bool FaxEnabled { get; set; }
        public bool FaxRequired { get; set; }
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Fax")]
        public string Fax { get; set; }

        public bool NewsletterEnabled { get; set; }
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Newsletter")]
        public bool Newsletter { get; set; }

        public bool AcceptPrivacyPolicyEnabled { get; set; }

        //time zone
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.TimeZone")]
        public string TimeZoneId { get; set; }
        public bool AllowUsersToSetTimeZone { get; set; }
        public IList<SelectListItem> AvailableTimeZones { get; set; }

        public bool HoneypotEnabled { get; set; }
        public bool DisplayCaptcha { get; set; }

        public IList<UserAttributeModel> UserAttributes { get; set; }
        public IList<NewsletterSimpleCategory> NewsletterCategories { get; set; }


        public DateTime? RegisterDate { get; set; }

        /// <summary>
        /// کاربر ثبت کننده
        /// </summary>
        [ScaffoldColumn(false)]
        public long? RegisterUserId { get; set; }

        /// <summary>
        /// تاریخ آخرین ویرایش
        /// </summary>
        [ScaffoldColumn(false)]
        public DateTime? LastUpdate { get; set; }

        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        [ScaffoldColumn(false)]
        public long? UpdateUserId { get; set; }

        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Description")]
        public string Description { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        [ScaffoldColumn(false)]
        public YesNo? Status { get; set; }

        /************************************************************************/

        /// <summary>
        /// شماره پرونده
        /// </summary>
        //[ScaffoldColumn(false)]
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.FileNumber")]
        public string FileNumber { get; set; }

        /// <summary>
        /// شماره ملی
        /// </summary>
        [Required(ErrorMessage = "Account.Jobseeker.Fields.NationalCode.Required")]
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.NationalCode")]
        public string NationalCode { get; set; }

        [Required(ErrorMessage = "Account.Jobseeker.Fields.EnrollDate.Required")]
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.EnrollDate")]
        public DateTime? EnrollDate { get; set; }

        [Required(ErrorMessage = "Account.Jobseeker.Fields.EnrollTime.Required")]
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.EnrollTime")]
        public string EnrollTime { get; set; }

        /// <summary>
        /// نام پدر
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.FatherName")]
        [DisplayFormat(NullDisplayText = "---")]
        public string FatherName { get; set; }

        /// <summary>
        /// محل تولد
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.BirthPlace")]
        public string BirthPlace { get; set; }

        /// <summary>
        /// شماره شناسنامه
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.SsnId")]
        public string SsnId { get; set; }

        /// <summary>
        /// سریال شناسنامه
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.SsnSerial")]
        public string SsnSerial { get; set; }

        /// <summary>
        /// وضعیت تاهل
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Marital")]
        public MaritalStatus? Marital { get; set; }

        /// <summary>
        /// تعداد فرزندان
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.ChildCount")]
        public int? ChildCount { get; set; }

        /// <summary>
        /// سرپرست خانوار
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.HeadHousehold")]
        public YesNo? HeadHousehold { get; set; }

        /// <summary>
        /// تعداد افراد تحت تکفل
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.DependentPersonsCount")]
        public int? DependentPersonsCount { get; set; }

        /// <summary>
        /// دین
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.ReligionId")]
        public ReligionTypes? ReligionId { get; set; }

        /// <summary>
        /// مذهب
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.ReligionName")]
        public string ReligionName { get; set; }

        /// <summary>
        /// تابعیت
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Nationality")]
        public Nationalities? Nationality { get; set; }

        /// <summary>
        /// تحت پوشش
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.CoveredBy")]
        public CoveredByTypes? CoveredBy { get; set; }

        /// <summary>
        /// وضعیت سربازی
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.SoldierState")]
        public SoldierStates? SoldierState { get; set; }

        /// <summary>
        /// علت معافیت
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.ExemptionCase")]
        [DisplayFormat(NullDisplayText = "---")]
        public string ExemptionCase { get; set; }

        /// <summary>
        /// نوع پوشش لباس
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.CoverageType")]
        public CoverageTypes? CoverageType { get; set; }

        /// <summary>
        /// قد براساس سانتیمتر
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Stature")]
        public int? Stature { get; set; }

        /// <summary>
        /// وزن برحسب کیلو
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Weight")]
        public int? Weight { get; set; }

        /// <summary>
        /// وضعیت فعالیت فعلی
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.CurrentActivityStatus")]
        public ActivityStatus? CurrentActivityStatus { get; set; }

        /// <summary>
        /// وشعیت قبل از جستجو
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.BeforeSearchState")]
        public BeforeSearchStates? BeforeSearchState { get; set; }

        /// <summary>
        /// محل بازنشستگی
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.RetirementPlace")]
        public string RetirementPlace { get; set; }

        /// <summary>
        /// دارای نامه نهادی
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.IsInstitutionalLetter")]
        public YesNo? IsInstitutionalLetter { get; set; }

        /// <summary>
        /// نامه نهادی از
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.InstitutionalLetterCode")]
        public int? InstitutionalLetterCode { get; set; }

        /******************************* آدرس و تماس و شماره تلفن های ضروری ***************************/

        [ScaffoldColumn(false)]
        //[ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        /// <summary>
        /// استان
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.ProvinceId")]
        public int ProvinceId { get; set; }

        //[ForeignKey("ProvinceId")]
        [ScaffoldColumn(false)]
        public virtual Province Province { get; set; }

        /// <summary>
        /// شهرستان
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.ShahrestanId")]
        public int ShahrestanId { get; set; }

        //[ForeignKey("SharestanId")]
        [ScaffoldColumn(false)]
        public virtual Shahrestan Shahrestan { get; set; }

        /// <summary>
        /// بخش
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.SectionId")]
        public int SectionId { get; set; }

        //[ForeignKey("SectionId")]
        [ScaffoldColumn(false)]
        public virtual Section Section { get; set; }

        /// <summary>
        /// خیابان اصلی
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.MainStreet")]
        public string MainStreet { get; set; }

        /// <summary>
        /// آدرس
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Address")]
        public string Address { get; set; }

        /// <summary>
        /// شماره همراه
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.MobileNumber")]
        [RegularExpression("09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}",
            ErrorMessage = "Account.Jobseeker.Fields.WrongMobileNumber")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// کد پستی
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.PostalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        ///  تصویر آواتار
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.PicturePath")]
        public string PicturePath { get; set; }

        /// <summary>
        /// تلفنهای ضروری
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual ICollection<EssentialPhone> EssentialPhones { get; set; }

        /*************************************** وضعیت جسمانی و بیمه******************************************/

        /// <summary>
        /// وضعیت ایثارگری
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.SacrificeState")]
        public SacrificeStates? SacrificeState { get; set; }

        /// <summary>
        /// وضعیت جسمانی
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.PhysicalConditionTypes")]
        public PhysicalConditionTypes? PhysicalConditionTypes { get; set; }

        /// <summary>
        /// نوع معلولیت
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.DisabledType")]
        public DisabledTypes? DisabledType { get; set; }

        /// <summary>
        /// درصد معلولیت
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.DisabledPercent")]
        public int? DisabledPercent { get; set; }

        /// <summary>
        /// وضعیت سابقه بیمه
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.InsuranceHistoryState")]
        public YesNo? InsuranceHistoryState { get; set; }

        /// <summary>
        /// سازمان بیمه کننده
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.InsuranceOrganization")]
        public InsuranceOrganizations? InsuranceOrganization { get; set; }

        /// <summary>
        ///  سابقه بیمه برحسب ماه
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.InsuranceNumber")]
        public string InsuranceNumber { get; set; }

        /// <summary>
        /// سابقه بیمه
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.InsuranceHistory")]
        public int InsuranceHistory { get; set; }

        /// <summary>
        /// مشمول بیمه بیکاری
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.UnemploymentInsurance")]
        public YesNo? UnemploymentInsurance { get; set; }

        /// <summary>
        /// تاریخ شروع مستمری
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.StartDatePension")]
        public DateTime? StartDatePension { get; set; }

        /// <summary>
        /// تاریخ پایان مستمری
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.EndDatePension")]
        public DateTime? EndDatePension { get; set; }


        /*********************************** بیماری های خاص *************************************/

        /// <summary>
        /// بیماری خاص
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.IsSpecialDisease")]
        public YesNo? IsSpecialDisease { get; set; }

        /// <summary>
        /// بیماری های خاص
        /// </summary>
        //[AtiehJobResourceDisplayName("بیماری خاص")]
        [ScaffoldColumn(false)]
        public virtual ICollection<SpecialDisease> SpecialDiseases { get; set; }

        /// <summary>
        /// آیا اخذ سوء پیشینه و یا عدم اعتیاد برای شما مقدور است؟
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.CanHistoryAbuse")]
        public YesNo? CanHistoryAbuse { get; set; }

        /// <summary>
        /// درصورت خیر به چه دلیل
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.HistoryAbuseDescription")]
        public string HistoryAbuseDescription { get; set; }

        /******************************************* تحصیلات ********************************************************/

        /// <summary>
        /// مقطع تحصیلی
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.EducationLevel")]
        public int? EducationLevel { get; set; }

        /// <summary>
        /// مشغول به تحصیل
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.IsStudent")]
        public YesNo? IsStudent { get; set; }

        /// <summary>
        ///  رشته تحصیلی
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.EducationCourse")]
        public string EducationCourse { get; set; }

        /// <summary>
        /// گرایش تحصیلی
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.EducationTrend")]
        public int? EducationTrend { get; set; }

        /// <summary>
        /// استان محل تحصیل
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.EducationProvinceId")]
        public int? EducationProvinceId { get; set; }

        /// <summary>
        /// نوع دانشگاه
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.UniversityType")]
        public string UniversityType { get; set; }

        /// <summary>
        /// نام دانشگاه
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.UniversityName")]
        public string UniversityName { get; set; }

        /// <summary>
        /// تاریخ اخذ مدرک
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.GraduationDate")]
        public DateTime? GraduationDate { get; set; }

        /// <summary>
        /// معدل
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.Average")]
        public float? Average { get; set; }

        /// <summary>
        /// تعداد واحد گذرانده
        /// </summary>
        [AtiehJobResourceDisplayName("Account.Jobseeker.Fields.PassUnitCount")]
        public int? PassUnitCount { get; set; }

        /// <summary>
        /// شناسه کاربری
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual int UserId { get; set; }

        [ScaffoldColumn(false)]
        public virtual Core.Domain.Users.User User { get; set; }
    }
}
