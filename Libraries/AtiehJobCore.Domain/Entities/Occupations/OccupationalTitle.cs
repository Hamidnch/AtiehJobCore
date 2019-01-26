using System.Collections.Generic;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.JobOpportunities;
using AtiehJobCore.Domain.Entities.Jobseekers;

namespace AtiehJobCore.Domain.Entities.Occupations
{
    /// <summary>
    /// عنوان شغلی
    /// </summary>
    public class OccupationalTitle : BaseEntity, IAuditableEntity
    {
        public OccupationalTitle() { }
        public OccupationalTitle(string name, int? occupationalGroupCode)
        {
            Name = name;
            OccupationalGroupCode = occupationalGroupCode;
        }
        /// <summary>
        /// عنوان شغل
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// کد گروه شغلی
        /// </summary>
        public int? OccupationalGroupCode { get; set; }

        public virtual OccupationalGroup OccupationalGroup { get; set; }

        public virtual ICollection<JobDemand> JobDemands { get; set; }
        public virtual ICollection<OccupationalHistory> OccupationalHistories { get; set; }
        public virtual ICollection<JobOpportunity> JobOpportunities { get; set; }
    }
}