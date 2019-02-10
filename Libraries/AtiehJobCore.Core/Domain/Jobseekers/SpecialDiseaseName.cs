using System.Collections.Generic;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Jobseekers
{
    /// <summary>
    /// نام بیماری های خاص
    /// </summary>
    public class SpecialDiseaseName : BaseMongoEntity
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
