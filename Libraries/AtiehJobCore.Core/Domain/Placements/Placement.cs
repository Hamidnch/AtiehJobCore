using System;
using System.Collections.Generic;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Address;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Placements
{
    /// <summary>
    /// کاریابی
    /// </summary>
    public class Placement : BaseMongoEntity, IAuditableEntity
    {
        public Placement() { }

        public Placement(string fileNumber, string name, string managerName, string managerNationalCode,
            ActivityTypes? activityType, LicenseTypes? licenseType, DateTimeOffset? licenseDate,
            string licenseNumber, string licenseLocation, int? provinceCode, string workshopCode,
            string mobile, string email, int userId)
        {
            FileNumber = fileNumber;
            Name = name;
            ManagerName = managerName;
            ManagerNationalCode = managerNationalCode;
            ActivityType = activityType;
            LicenseType = licenseType;
            LicenseDate = licenseDate;
            LicenseNumber = licenseNumber;
            LicenseLocation = licenseLocation;
            ProvinceCode = provinceCode;
            WorkshopCode = workshopCode;
            MobileNumber = mobile;
            Email = email;
            UserId = userId;
        }
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
        /// نام کاریابی
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// نام مدیر کاریابی
        /// </summary>
        public string ManagerName { get; set; }
        /// <summary>
        /// کد ملی مدیر
        /// </summary>
        public string ManagerNationalCode { get; set; }
        /// <summary>
        /// نوع فعالیت
        /// </summary>
        public ActivityTypes? ActivityType { get; set; }
        /// <summary>
        /// نوع مجوز
        /// </summary>
        public LicenseTypes? LicenseType { get; set; }
        /// <summary>
        /// تاریخ مجوز
        /// </summary>
        public DateTimeOffset? LicenseDate { get; set; }
        /// <summary>
        /// شماره مجوز صادر شده از وزارت کار
        /// </summary>
        public string LicenseNumber { get; set; }
        /// <summary>
        /// محل دریافت مجوز
        /// </summary>
        public string LicenseLocation { get; set; }
        /// <summary>
        ///  استان دریافت مجوز
        /// </summary>
        public int? ProvinceCode { get; set; }

        public virtual Province Province { get; set; }
        /// <summary>
        /// کد کارگاه
        /// </summary>
        public string WorkshopCode { get; set; }

        public byte[] Timestamp { get; set; }

        /***********************  آدرس های کارفرما *************************/
        /// <summary>
        /// لیست آدرس های کاریاب
        /// </summary>
        public virtual ICollection<PlacementAddress> PlacementAddresses { get; set; }

        /*********************** حسابهای بانکی کاریابی ها *****************/
        /// <summary>
        /// لیست حسابهای های کاریاب
        /// </summary>
        public virtual ICollection<PlacementBankAccount> PlacementBankAccounts { get; set; }
        /***************** ارتباط با جدول کاربر ********************/
        /// <summary>
        /// شناسه کاربری
        /// </summary>
        public int UserId { get; set; }
        public virtual User User { get; set; }
        /***************** ارتباط با جدول کاربر ********************/
    }
}
