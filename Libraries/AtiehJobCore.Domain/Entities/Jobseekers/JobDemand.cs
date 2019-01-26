using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.Occupations;

namespace AtiehJobCore.Domain.Entities.Jobseekers
{
    /// <summary>
    /// مشاغل درخواستی
    /// </summary>
    public class JobDemand : BaseEntity, IAuditableEntity
    {
        public JobDemand() { }

        public JobDemand(int occupationalGroupCode, int occupationalTitleCode, int jobseekerId)
        {
            OccupationalGroupCode = occupationalGroupCode;
            OccupationalTitleCode = occupationalTitleCode;
            JobseekerId = jobseekerId;
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

        //public IDictionary<OccupationalGroup, OccupationalTitle> DemandJobDictionary { get; set; }

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