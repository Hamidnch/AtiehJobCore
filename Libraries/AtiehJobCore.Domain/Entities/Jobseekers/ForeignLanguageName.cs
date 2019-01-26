using System.Collections.Generic;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;

namespace AtiehJobCore.Domain.Entities.Jobseekers
{
    /// <summary>
    /// نام زبان خارجه
    /// </summary>
    public class ForeignLanguageName : BaseEntity, IAuditableEntity
    {
        public ForeignLanguageName() { }

        public ForeignLanguageName(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        /// <summary>
        /// ارتباط با کلاس زبان های خارجه کارجو
        /// </summary>
        public virtual ICollection<ForeignLanguage> ForeignLanguages { get; set; }
    }
}
