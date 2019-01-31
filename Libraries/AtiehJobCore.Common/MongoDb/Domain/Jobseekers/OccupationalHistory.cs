using AtiehJobCore.Common.MongoDb.Domain.Occupations;

namespace AtiehJobCore.Common.MongoDb.Domain.Jobseekers
{
    /// <summary>
    /// سوابق شغلی
    /// </summary>
    public class OccupationalHistory : BaseMongoEntity
    {
        public OccupationalHistory() { }

        public OccupationalHistory(int occupationalGroupCode, int occupationalTitleCode,
             string organizationName, byte? experienceWork, string address, string phone,
             string leaveWorkReason, int jobSeekerId)
        {
            OccupationalGroupCode = occupationalGroupCode;
            OccupationalTitleCode = occupationalTitleCode;
            OrganizationName = organizationName;
            ExperienceWork = experienceWork;
            Address = address;
            Phone = phone;
            LeaveWorkReason = leaveWorkReason;
            JobseekerId = jobSeekerId;
        }
        /// <summary>
        /// کد گروه شغلی
        /// </summary>
        public int? OccupationalGroupCode { get; set; }
        public virtual OccupationalGroup OccupationalGroup { get; set; }

        /// <summary>
        /// کد عنوان شغل
        /// </summary>
        public int? OccupationalTitleCode { get; set; }
        public virtual OccupationalTitle OccupationalTitle { get; set; }

        /// <summary>
        /// نام موسسه محل کار
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// سابقه کار به ماه
        /// </summary>
        public byte? ExperienceWork { get; set; }

        /// <summary>
        /// آدرس محل کار
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// شماره تماس
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// علت ترک کار
        /// </summary>
        public string LeaveWorkReason { get; set; }

        /// <summary>
        /// کد کارجو
        /// </summary>
        public int JobseekerId { get; set; }

        /// <summary>
        /// ارتباط با کلاس کارجو
        /// Navigation Property
        /// </summary>
        public virtual Jobseeker Jobseeker { get; set; }
    }
}
