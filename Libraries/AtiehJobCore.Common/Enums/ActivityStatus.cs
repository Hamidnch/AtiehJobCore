using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    /// <summary>
    /// وضعیت فعلی
    /// </summary>
    public enum ActivityStatus : byte
    {
        /// <summary>
        /// فاقد کار
        /// </summary>
        [Display(Name = "فاقد کار")]
        NoWork = 1,

        /// <summary>
        /// شاغل جویای کار
        /// </summary>
        [Display(Name = "شاغل و جویای کار بهتر")]
        EmployedJobseeker = 2
    }
}
