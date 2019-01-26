using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    /// <summary>
    /// وضعیت ایثارگری
    /// </summary>
    public enum SacrificeStates : byte
    {
        /// <summary>
        /// آزاده
        /// </summary>
        [Display(Name = "آزاده")]
        Free = 1,

        /// <summary>
        /// جانباز
        /// </summary>
        [Display(Name = "جانباز")]
        Veteran = 2,

        /// <summary>
        /// خانواده ایثارگر
        /// </summary>
        [Display(Name = "خانواده ایثارگر")]
        Altruist = 3,

        /// <summary>
        /// رزمنده
        /// </summary>
        [Display(Name = "رزمنده")]
        Combatant = 4,

        /// <summary>
        /// خانواده شهید
        /// </summary>
        [Display(Name = "خانواده شهید")]
        Martyr = 5
    }
}
