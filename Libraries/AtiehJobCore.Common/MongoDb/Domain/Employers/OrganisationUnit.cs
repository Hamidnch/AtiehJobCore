using System.Collections.Generic;

namespace AtiehJobCore.Common.MongoDb.Domain.Employers
{
    /// <summary>
    /// واحد سازمانی
    /// </summary>
    public class OrganizationUnit : BaseMongoEntity
    {
        public OrganizationUnit() { }

        public OrganizationUnit(string type)
        {
            Type = type;
        }
        public string Type { get; set; }

        /// <summary>
        /// ارتباط با کلاس کارفرما
        /// </summary>
        public virtual ICollection<Employer> Employers { get; set; }
    }
}
