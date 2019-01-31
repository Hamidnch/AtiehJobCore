using System.Collections.Generic;
using AtiehJobCore.Common.MongoDb.Domain.JobOpportunities;
using AtiehJobCore.Common.MongoDb.Domain.Jobseekers;

namespace AtiehJobCore.Common.MongoDb.Domain.Occupations
{
    /// <summary>
    /// عنوان شغلی
    /// </summary>
    public class OccupationalTitle : BaseMongoEntity
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
