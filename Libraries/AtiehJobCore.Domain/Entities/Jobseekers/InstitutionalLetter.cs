using System.Collections.Generic;
using AtiehJobCore.Domain.Entities.Common;

namespace AtiehJobCore.Domain.Entities.Jobseekers
{
    /// <summary>
    /// نامه نهادی
    /// </summary>
    public class InstitutionalLetter : BaseEntity
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
