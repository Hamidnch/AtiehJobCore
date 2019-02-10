using System.Collections.Generic;
using AtiehJobCore.Core.Domain.JobOpportunities;
using AtiehJobCore.Core.Domain.Jobseekers;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Occupations
{
    /// <summary>
    /// گروه شغلی
    /// </summary>
    public class OccupationalGroup : BaseMongoEntity
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
