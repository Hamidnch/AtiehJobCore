using System.Collections.Generic;

namespace AtiehJobCore.Common.MongoDb.Domain.Jobseekers
{
    /// <summary>
    /// نامه نهادی
    /// </summary>
    public class InstitutionalLetter : BaseMongoEntity
    {
        public InstitutionalLetter() { }

        public InstitutionalLetter(string from)
        {
            From = from;
        }
        public string From { get; set; }
        /// <summary>
        /// ارتباط با کلاس کارجو
        /// </summary>
        public virtual ICollection<Jobseeker> Jobseekers { get; set; }
    }
}
