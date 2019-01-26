using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    public enum InsuranceOrganizations : byte
    {
        /// <summary>
        /// تامین اجتماعی
        /// </summary>
        [Display(Name = "تامین اجتماعی")]
        SocialSecurity = 1,

        /// <summary>
        /// نیروهای مسلح
        /// </summary>
        [Display(Name = "نیروهای مسلح")]
        ArmedForces = 2,

        /// <summary>
        /// خدمات درمانی
        /// </summary>
        [Display(Name = "خدمات درمانی")]
        TreatmentServices = 3,
        /// <summary>
        /// سایر
        /// </summary>
        [Display(Name = "سایر")]
        Other = 5,
        /// <summary>
        /// شرکت نفت
        /// </summary>
        [Display(Name = "شرکت نفت")]
        OilCompany = 6,
        /// <summary>
        /// بانک صادرات
        /// </summary>
        [Display(Name = "بانک صادرات")]
        SaderatBank = 7,
        /// <summary>
        /// بدون بیمه
        /// </summary>
        [Display(Name = "بدون بیمه")]
        WithoutInsurance = 8,
        /// <summary>
        /// سلامت
        /// </summary>
        [Display(Name = "سلامت")]
        Health = 9

    }
}
