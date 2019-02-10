using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Core.Enums
{
    public enum SoldierStates : byte
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
        /// معافیت دائم پزشکی
        /// </summary>
        [Display(Name = "معافیت دائم پزشکی")]
        MedicalExemption = 3,

        /// <summary>
        /// معافیت موقت پزشکی
        /// </summary>
        [Display(Name = "معافیت موقت پزشکی")]
        TempExemption = 4,

        /// <summary>
        /// معافیت تحصیلی
        /// </summary>
        [Display(Name = "معافیت تحصیلی ")]
        EducationExemtion = 5,

        /// <summary>
        /// معافیت کفالت
        /// </summary>
        [Display(Name = "معافیت کفالت")]
        BailExemption = 6,

        /// <summary>
        /// سایر
        /// </summary>
        [Display(Name = "سایر")]
        Other = 7
    }
}
