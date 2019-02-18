using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Core.Enums
{
    /// <summary>
    /// وضعیت جاری کارجو
    /// </summary>
    public enum JobseekerState
    {
        [Display(Name = "ثبت نام اولیه")]
        PrimaryRegisteration = 0
    }
}
