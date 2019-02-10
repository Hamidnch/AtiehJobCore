using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Core.Enums
{
    /// <summary>
    /// وضعیت پیش از جستجو
    /// </summary>
    public enum BeforeSearchStates : byte
    {
        /// <summary>
        /// شاغل
        /// </summary>
        [Display(Name = "شاغل")]
        Employed = 1,
        /// <summary>
        /// محصل
        /// </summary>
        [Display(Name = "محصل/دانشجو")]
        Student = 2,
        /// <summary>
        /// خانه دار
        /// </summary>
        [Display(Name = "خانه دار")]
        Housewife = 3,
        /// <summary>
        /// دارای درآمد بدون کسب و کار
        /// </summary>
        [Display(Name = "دارای درآمد بدون کسب و کار")]
        NoWorkWithMoney = 4,
        /// <summary>
        /// مقرری بگیر بیمه بیکاری
        /// </summary>
        [Display(Name = "مقرری بگیر بیمه بیکاری")]
        Catchappointments = 5,
        /// <summary>
        /// بازنشسته
        /// </summary>
        [Display(Name = "بازنشسته")]
        Retired = 6,
        /// <summary>
        /// بیکار
        /// </summary>
        [Display(Name = "بیکار")]
        NoWork = 7
    }
}
