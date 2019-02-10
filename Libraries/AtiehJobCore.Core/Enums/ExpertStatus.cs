using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Core.Enums
{

    /// <summary>
    /// رتبه وضعیت
    /// </summary>
    public enum ExpertStatus : byte
    {
        /// <summary>
        /// عالی
        /// </summary>
        [Display(Name = "عالی")]
        Best = 1,

        /// <summary>
        /// خوب
        /// </summary>
        [Display(Name = "خوب")]
        Good = 2,

        /// <summary>
        /// متوسط
        /// </summary>
        [Display(Name = "متوسط")]
        Middle = 3,

        /// <summary>
        /// ضعیف
        /// </summary>
        [Display(Name = "ضعیف")]
        Week = 4
    }

}
