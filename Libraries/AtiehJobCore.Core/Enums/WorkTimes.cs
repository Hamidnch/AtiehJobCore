using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Core.Enums
{
    public enum WorkTimes
    {
        /// <summary>
        /// پروژه ای
        /// </summary>
        [Display(Name = "پروژه ای")]
        Project = 1,
        /// <summary>
        /// روزکاری
        /// </summary>
        [Display(Name = "روزکاری")]
        WorkDay = 2,
        /// <summary>
        /// شبکاری
        /// </summary>
        [Display(Name = "شبکاری")]
        WorkNight = 3,
        /// <summary>
        /// شبانه روزی
        /// </summary>
        [Display(Name = "شبانه روزی")]
        WorkDailyNightly = 4,
        /// <summary>
        /// شیفتی
        /// </summary>
        ///
        [Display(Name = "شیفتی")]
        Shift = 5,
        /// <summary>
        /// فرقی نمی کند
        /// </summary>
        [Display(Name = "فرقی نمی کند")]
        NoDiffer = 6,
        /// <summary>
        /// نوبت کاری  
        /// </summary>
        [Display(Name = "نوبت کاری")]
        WorkNobat = 7,
        /// <summary>
        /// نیمه وقت بعدازظهر
        /// </summary>
        [Display(Name = "نیمه وقت بعدازظهر")]
        PartTimeAfternoon = 8,
        /// <summary>
        /// نیمه وقت صبح
        /// </summary>
        [Display(Name = "نیمه وقت صبح")]
        PartTimeMorning = 9
    }
}
