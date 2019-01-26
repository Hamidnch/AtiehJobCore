using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    public enum ActivityTypes
    {
        /// <summary>
        /// داخلی
        /// </summary>
        [Display(Name = "داخلی")]
        Internal = 1,
        /// <summary>
        /// خارجی
        /// </summary>
        [Display(Name = "خارجی")]
        External = 2,
        /// <summary>
        /// داخلی / خارجی
        /// </summary>
        [Display(Name = "داخلی/خارجی")]
        InternalExternal = 3
    }
}
