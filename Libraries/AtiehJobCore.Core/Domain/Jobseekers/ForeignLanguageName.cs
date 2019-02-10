using System.Collections.Generic;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Jobseekers
{
    /// <summary>
    /// نام زبان خارجه
    /// </summary>
    public class ForeignLanguageName : BaseMongoEntity
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
