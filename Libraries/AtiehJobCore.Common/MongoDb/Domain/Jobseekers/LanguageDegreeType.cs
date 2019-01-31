using System.Collections.Generic;

namespace AtiehJobCore.Common.MongoDb.Domain.Jobseekers
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
