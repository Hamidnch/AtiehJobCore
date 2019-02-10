using System.Collections.Generic;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Jobseekers
{
    public class LanguageDegreeType : BaseMongoEntity
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
