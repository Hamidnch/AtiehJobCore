using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Core.Enums
{
    public enum ReligionTypes : byte
    {
        /// <summary>
        /// اسلام
        /// </summary>
        [Display(Name = "اسلام")]
        Islam = 1,

        /// <summary>
        /// مسیحیت
        /// </summary>
        [Display(Name = "مسیحیت")]
        Church = 2,

        /// <summary>
        /// یهود
        /// </summary>
        [Display(Name = "یهودیت")]
        Jews = 3,

        /// <summary>
        /// زرتشت
        /// </summary>
        [Display(Name = "زرتشت")]
        Zoroaster = 4,

        /// <summary>
        /// بودا
        /// </summary>
        [Display(Name = "سایر")]
        Buddha = 5,
    }
}
