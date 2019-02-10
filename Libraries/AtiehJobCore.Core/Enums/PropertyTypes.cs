using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Core.Enums
{
    /// <summary>
    /// نوع مالکیت
    /// </summary>
    public enum PropertyTypes
    {
        [Display(Name = "دولتی")]
        Governmental = 1,
        [Display(Name = "خصوصی")]
        Private = 2,
        [Display(Name = "تعاونی")]
        Cooperative = 3
    }
}
