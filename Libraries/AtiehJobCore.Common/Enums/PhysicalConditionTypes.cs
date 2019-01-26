using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    /// <summary>
    /// وضعیت جسمانی
    /// </summary>
    public enum PhysicalConditionTypes : byte
    {
        /// <summary>
        /// سالم
        /// </summary>
        [Display(Name = "سالم")]
        Healthy = 1,

        /// <summary>
        /// معلول
        /// </summary>
        [Display(Name = "معلول")]
        Disabeld = 2
    }
}
