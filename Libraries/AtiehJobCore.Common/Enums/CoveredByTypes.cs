using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    public enum CoveredByTypes : byte
    {
        /// <summary>
        /// تامین اجتماعی
        /// </summary>
        [Display(Name = "تامین اجتماعی")]
        SocialSecurity = 1,

        /// <summary>
        /// بهزیستی
        /// </summary>
        [Display(Name = "بهزیستی")]
        Welfare = 2,

        /// <summary>
        /// کمیته امداد
        /// </summary>
        [Display(Name = "کیمته امداد")]
        AidCommittee = 3,

        /// <summary>
        /// خیریه
        /// </summary>
        [Display(Name = "خیریه")]
        Charity = 4,
        /// <summary>
        /// سایر
        /// </summary>
        [Display(Name = "سایر")]
        Other = 5
    }
}
