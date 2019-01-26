using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    /// <summary>
    /// گویش
    /// </summary>
    public enum Dialects : byte
    {
        /// <summary>
        /// فرقی نمی کند
        /// </summary>
        [Display(Name = "فرقی نمی کند")]
        NoDiffer = 1,
        /// <summary>
        /// دارای لهجه
        /// </summary>
        [Display(Name = "دارای لهجه")]
        WithAccent = 2,
        /// <summary>
        /// بدون لهجه
        /// </summary>
        [Display(Name = "بدون لهجه")]
        WithoutAccent = 3
    }
}
