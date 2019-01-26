using System;
using System.Collections.Generic;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.Enums;
using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.Identity;
using AtiehJobCore.Domain.Entities.JobOpportunities;

namespace AtiehJobCore.Domain.Entities.Employers
{
    /// <summary>
    /// کارفرما
    /// </summary>
    public class Employer : BaseEntity, IAuditableEntity
    {
        #region Constructor

        public Employer() { }

        public Employer(string fileNumber, int organizationUnitCode,
            string humanApplicantUnit, DateTimeOffset enrollDate, string enrollTime,
            ActivityFields? activityField, string activityType, string unitCode,
            string melliCode, string name, string family, bool? insuranceStatus,
            string insuranceCode, string phone, string mobile, string webSite, string email,
            PropertyTypes? propertyType, int? introductionMethodCode, Ranks? rank,
            bool? beforeDispatchCoordination)
        {
            FileNumber = fileNumber;
            OrganizationUnitCode = organizationUnitCode;
            HumanApplicantUnit = humanApplicantUnit;
            EnrollDate = enrollDate;
            EnrollTime = enrollTime;
            ActivityField = activityField;
            ActivityType = activityType;
            UnitCode = unitCode;
            NationalCode = melliCode;
            Name = name;
            Family = family;
            InsuranceStatus = insuranceStatus;
            InsuranceCode = insuranceCode;
            Phone = phone;
            MobileNumber = mobile;
            WebSite = webSite;
            Email = email;
            PropertyType = propertyType;
            IntroductionMethodCode = introductionMethodCode;
            Rank = rank;
            BeforeDispatchCoordination = beforeDispatchCoordination;
        }
        #endregion

        #region Property
        /// <summary>
        /// شماره پرونده
        /// </summary>
        public string FileNumber { get; set; }
        /// <summary>
        /// تلفن همراه
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// کد نوع واحد سازمانی
        /// </summary>
        public int? OrganizationUnitCode { get; set; }
        public OrganizationUnit OrganizationUnit { get; set; }
        /// <summary>
        /// واحد متقاضی نیرو
        /// </summary>
        public string HumanApplicantUnit { get; set; }

        /// <summary>
        /// تاریخ ثبت نام
        /// </summary>
        public DateTimeOffset EnrollDate { get; set; }

        /// <summary>
        /// ساعت ثبت نام 
        /// </summary>
        public string EnrollTime { get; set; }

        /// <summary>
        /// زمینه فعالیت
        /// </summary>
        public ActivityFields? ActivityField { get; set; }
        /// <summary>
        /// نوع فعالیت
        /// </summary>
        public string ActivityType { get; set; }
        /// <summary>
        /// کد واحد
        /// </summary>
        public string UnitCode { get; set; }
        /// <summary>
        /// کد ملی
        /// </summary>
        public string NationalCode { get; set; }

        /// <summary>
        /// نام کارجو
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string Family { get; set; }
        /// <summary>
        /// وضعیت بیمه
        /// </summary>
        public bool? InsuranceStatus { get; set; }
        /// <summary>
        /// کد بیمه
        /// </summary>
        public string InsuranceCode { get; set; }
        /// <summary>
        /// تلفن ثابت
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// وب سایت
        /// </summary>
        public string WebSite { get; set; }

        /// <summary>
        /// نوع مالکیت
        /// </summary>
        public PropertyTypes? PropertyType { get; set; }
        /// <summary>
        /// کد نحوه آشنایی با مرکز
        /// </summary>
        public int? IntroductionMethodCode { get; set; }
        public IntroductionMethod IntroductionMethod { get; set; }
        /// <summary>
        /// رتبه کارفرما
        /// </summary>
        public Ranks? Rank { get; set; }

        /// <summary>
        /// هماهنگی قبل از اعزام
        /// </summary>
        public bool? BeforeDispatchCoordination { get; set; }
        /// <summary>
        /// وضعیت جاری
        /// </summary>
        public string CurrentState { get; set; }


        /****************** آدرس ********************/
        /// <summary>
        /// لیست آدرس های کارفرما
        /// </summary>
        public virtual ICollection<EmployerAddress> EmployerAddresses { get; set; }

        /****************** سرویس ایاب و ذهاب ******************/
        /// <summary>
        /// لیست سرویس های ایاب و ذهاب
        /// </summary>
        public virtual ICollection<EmployerTransfer> EmployerTransfers { get; set; }

        /****************** فرصت های شغلی ***********************/
        /// <summary>
        /// لیستی از فرصت های شغلی
        /// </summary>
        public virtual ICollection<JobOpportunity> JobOpportunities { get; set; }

        /***************** ارتباط با جدول کاربر ********************/
        /// <summary>
        /// شناسه کاربری
        /// </summary>
        public int UserId { get; set; }
        public virtual User User { get; set; }
        /***************** ارتباط با جدول کاربر ********************/
        #endregion
    }
}
