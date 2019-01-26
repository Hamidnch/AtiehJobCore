using System.Collections.Generic;
using AtiehJobCore.Domain.Entities.Common;

namespace AtiehJobCore.Domain.Entities.Employers
{
    /// <summary>
    /// واحد سازمانی
    /// </summary>
    public class OrganizationUnit : BaseEntity
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
