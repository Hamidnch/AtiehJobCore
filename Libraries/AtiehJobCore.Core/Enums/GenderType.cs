using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Core.Enums
{
    public enum GenderType : byte
    {
        [Display(Name = "نامعلوم")]
        None = 0,
        [Display(Name = "مرد")]
        Male = 1,
        [Display(Name = "زن")]
        Female = 2
    }
}
