using System.Collections.Generic;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;

namespace AtiehJobCore.Domain.Entities.Jobseekers
{
    /// <summary>
    /// نام بیماری های خاص
    /// </summary>
    public class SpecialDiseaseName : BaseEntity, IAuditableEntity
    {

        public SpecialDiseaseName() { }

        public SpecialDiseaseName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// نام بیماری
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ارتباط با کلاس بیماری های خاص
        /// </summary>
        public virtual ICollection<SpecialDisease> SpecialDiseases { get; set; }
    }
}
