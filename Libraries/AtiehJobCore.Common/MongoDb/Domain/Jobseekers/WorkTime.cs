//using AtiehJob.Domain.Entities.Common;

//namespace AtiehJob.Domain.Entities.Job
//{
//    /// <summary>
//    /// نوع زمان کاری
//    /// </summary>
//    public class WorkTime : BaseEntity
//    {
//        /// <summary>
//        /// روزکاری
//        /// </summary>
//        public bool? WorkDay { get; set; }
//        /// <summary>
//        /// فرقی ندارد
//        /// </summary>
//        public bool? NoDiffer { get; set; }
//        /// <summary>
//        /// شبکاری
//        /// </summary>
//        public bool? WorkNight { get; set; }
//        /// <summary>
//        /// پروژه ای
//        /// </summary>
//        public bool? Project { get; set; }
//        /// <summary>
//        /// شیفتی
//        /// </summary>
//        public bool? Shift { get; set; }
//        /// <summary>
//        /// نیمه وقت صبح
//        /// </summary>
//        public bool? PartTimeMorning { get; set; }
//        /// <summary>
//        /// نیمه وقت بعدازظهر
//        /// </summary>
//        public bool? PartTimeAfternoon { get; set; }
//        /// <summary>
//        /// کد کارجو
//        /// </summary>
//        public int JobseekerId { get; set; }

//        /// <summary>
//        /// ارتباط با کلاس کارجو
//        /// Navigation Property
//        /// </summary>
//        public virtual Jobseeker Jobseeker { get; set; }
//    }
//}
