using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Core.Enums
{
    /// <summary>
    /// نوع پوشش
    /// </summary>
    public enum CoverageTypes : byte
    {
        /// <summary>
        /// اسپرت
        /// </summary>
        [Display(Name = "اسپرت")]
        Sport = 1,
        /// <summary>
        /// رسمی
        /// </summary>
        [Display(Name = "رسمی")]
        Formal = 2,

        /// <summary>
        /// مطابق با محیط کار
        /// </summary>
        [Display(Name = "مطابق با محیط کار")]
        EccordanceWork = 3
    }
}
