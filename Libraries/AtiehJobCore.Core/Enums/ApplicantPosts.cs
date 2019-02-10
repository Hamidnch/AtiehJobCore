using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Core.Enums
{
    /// <summary>
    /// سمت سفارش دهنده
    /// </summary>
    public enum ApplicantPosts : byte
    {
        /// <summary>
        /// كارمند  
        /// </summary>
        [Display(Name = "كارمند")]
        Employee = 1,
        /// <summary>
        /// كارمند كارگزيني  
        /// </summary>
        [Display(Name = "كارمند كارگزيني")]
        RecruiterEmployee = 2,
        /// <summary>
        /// مدير   
        /// </summary>
        [Display(Name = "مدير")]
        Manager = 3,
        /// <summary>
        /// مدير اداري   
        /// </summary>
        [Display(Name = "مدير اداري")]
        OfficeManager = 4,
        /// <summary>
        /// مدير داخلی   
        /// </summary>
        [Display(Name = "مدير داخلی")]
        DomesticManager = 5,
        /// <summary>
        /// مدير عامل   
        /// </summary>
        [Display(Name = "مدير عامل")]
        Ceo = 6,
        /// <summary>
        /// مدير کارخانه   
        /// </summary>
        [Display(Name = "مدير کارخانه")]
        FactoryManager = 7,
        /// <summary>
        /// مسئول   
        /// </summary>
        [Display(Name = "مسئول")]
        Responsible = 8,
        /// <summary>
        /// مسئول اداري
        /// </summary>
        [Display(Name = "مسئول اداري")]
        OfficeResponsible = 9
    }
}
