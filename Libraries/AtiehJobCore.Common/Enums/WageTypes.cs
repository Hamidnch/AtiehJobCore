using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    public enum WageTypes : byte
    {
        /// <summary>
        /// طبق قانون کار
        /// </summary>
        [Display(Name = "طبق قانون کار")]
        LabourLawBased = 1,

        /// <summary>
        /// توافقی
        /// </summary>
        [Display(Name = "توافقی")]
        Adaptive = 2,

        /// <summary>
        /// مبلغ به ریال
        /// </summary>
        [Display(Name = "مبلغ به ریال")]
        AmountBased = 3
    }
}
