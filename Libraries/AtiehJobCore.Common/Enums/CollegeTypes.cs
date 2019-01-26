using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    public enum CollegeTypes
    {
        /// <summary>
        /// دولتی
        /// </summary>
        [Display(Name = "دولتی")]
        Governmental = 1,

        /// <summary>
        /// آزاد
        /// </summary>
        [Display(Name = "آزاد")]
        Free = 2
    }
}
