using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    public enum LicenseTypes
    {
        /// <summary>
        /// حقیقی
        /// </summary>
        [Display(Name = "حقیقی")]
        Natural = 1,
        /// <summary>
        /// حقوقی
        /// </summary>
        [Display(Name = "حقوقی")]
        Legal = 2

    }
}
