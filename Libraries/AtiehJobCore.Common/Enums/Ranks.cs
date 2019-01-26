using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    /// <summary>
    /// رتبه
    /// </summary>
    public enum Ranks
    {
        [Display(Name = "عالی")]
        Best = 1,
        [Display(Name = "خوب")]
        Good = 2,
        [Display(Name = "متوسط")]
        Middle = 3,
        [Display(Name = "ضعیف")]
        Week = 4
    }
}
