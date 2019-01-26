using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    public enum UserType
    {
        /// <summary>
        /// مدیر سیستم
        /// </summary>
        //[Display(Name = "مدیر سیستم")]
        [ScaffoldColumn(false)]
        Admin = 0,
        /// <summary>
        /// کارجو
        /// </summary>
        [Display(Name = "کارجو")]
        Jobseeker = 1,

        /// <summary>
        /// کارفرما
        /// </summary>
        [Display(Name = "کارفرما")]
        Employer = 2,

        /// <summary>
        /// کاریاب
        /// </summary>
        [Display(Name = "کاریاب")]
        Placement = 3,
        /// <summary>
        /// سایر
        /// </summary>
        //[Display(Name = "سایر")]
        [ScaffoldColumn(false)]
        Other = 4,
    }
}
