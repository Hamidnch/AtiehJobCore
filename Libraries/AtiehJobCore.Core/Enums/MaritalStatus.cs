using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Core.Enums
{
    public enum MaritalStatus : byte
    {
        /// <summary>
        /// مجرد
        /// </summary>
        [Display(Name = "مجرد")]
        Single = 1,

        /// <summary>
        /// متاهل
        /// </summary>
        [Display(Name = "متاهل")]
        Married = 2,

        /// <summary>
        /// مطلقه
        /// </summary>
        [Display(Name = "مطلقه")]
        Divorced = 3,

        /// <summary>
        /// همسر فوت کرده
        /// </summary>
        [Display(Name = "همسر فوت کرده")]
        WifeDied = 4
    }
}
