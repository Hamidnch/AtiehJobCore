using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    public enum SoldierStates2 : byte
    {
        /// <summary>
        /// سرباز
        /// </summary>
        [Display(Name = "سرباز")]
        Soldier = 1,
        /// <summary>
        /// پایان خدمت
        /// </summary>
        [Display(Name = "پایان خدمت")]
        Serviced = 2,
        /// <summary>
        /// پایان خدمت یا معاف 
        /// </summary>
        [Display(Name = "پایان خدمت یا معاف")]
        ServicedOrExempt = 3,
        /// <summary>
        /// معافیت دائم پزشکی
        /// </summary>
        [Display(Name = "معافیت دائم پزشکی")]
        MedicalExemption = 4,
        /// <summary>
        /// معافیت موقت پزشکی
        /// </summary>
        [Display(Name = "معافیت موقت پزشکی")]
        TempExemption = 5,
        /// <summary>
        /// معافیت تحصیلی
        /// </summary>
        [Display(Name = "معافیت تحصیلی ")]
        EducationExemtion = 6,
        /// <summary>
        /// معافیت کفالت
        /// </summary>
        [Display(Name = "معافیت خاص")]
        SpecialExemption = 7,
        /// <summary>
        /// سایر
        /// </summary>
        [Display(Name = "سایر")]
        Other = 8,
        /// <summary>
        /// فرقی نمی کند
        /// </summary>
        [Display(Name = "فرقی نمی کند")]
        NoDiffer = 9
    }
}
