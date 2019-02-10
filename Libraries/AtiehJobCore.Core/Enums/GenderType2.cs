using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Core.Enums
{
    public enum GenderTypes2 : byte
    {
        /// <summary>
        /// زن
        /// </summary>
        [Display(Name = "زن")]
        Female = 0,
        /// <summary>
        /// مرد
        /// </summary>
        [Display(Name = "مرد")]
        Male = 1,
        /// <summary>
        /// زن و مرد
        /// </summary>
        [Display(Name = "زن و مرد")]
        MaleAndFemale = 2,
        /// <summary>
        /// ترجیحا زن
        /// </summary>
        [Display(Name = "ترجیحا زن")]
        FemaleRefer = 3,
        /// <summary>
        /// ترجیحا مرد
        /// </summary>
        [Display(Name = "ترجیحا مرد")]
        MaleRefer = 4
    }
}
