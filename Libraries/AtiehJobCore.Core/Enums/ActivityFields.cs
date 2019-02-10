using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Core.Enums
{
    /// <summary>
    /// زمینه های فعالیت
    /// </summary>
    public enum ActivityFields
    {
        [Display(Name = "صنعتی")]
        Industrial = 1,
        [Display(Name = "خدماتی")]
        Services = 2,
        [Display(Name = "کشاورزی")]
        Agriculture = 3
    }
}
