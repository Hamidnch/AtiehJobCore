using System.Collections.Generic;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Domain.Entities.Common;

namespace AtiehJobCore.Domain.Entities.Jobseekers
{
    public class LanguageDegreeType : BaseEntity, IAuditableEntity
    {
        public LanguageDegreeType() { }

        public LanguageDegreeType(string type)
        {
            Type = type;
        }
        /// <summary>
        /// نوع مهارت
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// ارتباط با کلاس زبان های خارجه کارجو
        /// </summary>
        public virtual ICollection<ForeignLanguage> ForeignLanguages { get; set; }
    }
}
