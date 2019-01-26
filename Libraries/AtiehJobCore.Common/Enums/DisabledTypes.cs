using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    public enum DisabledTypes : byte
    {
        [Display(Name = "پا")]
        Leg = 1,

        [Display(Name = "دست")]
        Hand = 2,

        [Display(Name = "چشم")]
        Eye = 3,

        [Display(Name = "گوش")]
        Ear = 4,

        [Display(Name = "ذهن")]
        Mind = 5,

        [Display(Name = "تکلم")]
        Speech = 6,

        [Display(Name = "سایر")]
        Other = 7
    }
}
