using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Core.Enums
{
    public enum Nationalities : byte
    {
        [Display(Name = "ایرانی")]
        Iranian = 1,

        [Display(Name = "افغانی")]
        Afghani = 2,

        [Display(Name = "پاکستانی")]
        Pakistanis = 3,

        [Display(Name = "آمریکایی")]
        American = 4,

        [Display(Name = "ایتالیایی")]
        Italian = 5,

        [Display(Name = "اسپانیایی")]
        Spanish = 6,

        [Display(Name = "هندی")]
        Indian = 7,

        [Display(Name = "بنگلادشی")]
        Bangladeshi = 8,

        [Display(Name = "بلژیکی")]
        Belgian = 9,

        [Display(Name = "فرانسوی")]
        French = 10,

        [Display(Name = "اماراتی")]
        Emarati = 11,

        [Display(Name = "دبی")]
        Dubai = 12,

        [Display(Name = "لبنانی")]
        Lebanon = 13,

        [Display(Name = "سوری")]
        Surrey = 14
    }
}
