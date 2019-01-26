using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    public enum YesNo : byte
    {
        /// <summary>
        /// بله
        /// </summary>
        [Display(Name = "بله")]
        Yes = 1,

        /// <summary>
        /// خیر
        /// </summary>
        [Display(Name = "خیر")]
        No = 2
    }
}
