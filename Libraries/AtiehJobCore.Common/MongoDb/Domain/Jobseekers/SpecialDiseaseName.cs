using System.Collections.Generic;

namespace AtiehJobCore.Common.MongoDb.Domain.Jobseekers
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
