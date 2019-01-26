using System.Collections.Generic;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.JobOpportunities;
using AtiehJobCore.Domain.Entities.Jobseekers;

namespace AtiehJobCore.Domain.Entities.Occupations
{
    /// <summary>
    /// گروه شغلی
    /// </summary>
    public class OccupationalGroup : BaseEntity, IAuditableEntity
    {
        public OccupationalGroup() { }
        public OccupationalGroup(string name)
        {
            Name = name;
        }

        /// <summary>
        /// نام گروه شغلی
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<OccupationalTitle> OccupationalTitles { get; set; }
        public virtual ICollection<JobDemand> JobDemands { get; set; }
        public virtual ICollection<OccupationalHistory> OccupationalHistories { get; set; }
        public virtual ICollection<JobOpportunity> JobOpportunities { get; set; }
    }
}