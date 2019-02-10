using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Core.Enums
{
    public enum SkillDegrees : byte
    {
        /// <summary>
        /// مبتدی
        /// </summary>
        [Display(Name = "مبتدی")]
        Beginner = 1,
        /// <summary>
        /// نیمه ماهر
        /// </summary>
        [Display(Name = "نیمه ماهر")]
        SemiSkilled = 2,
        /// <summary>
        /// ماهر
        /// </summary>
        [Display(Name = "ماهر")]
        Skilled = 3,
    }
}
