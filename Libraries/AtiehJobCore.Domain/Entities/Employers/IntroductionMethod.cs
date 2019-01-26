using System.Collections.Generic;
using AtiehJobCore.Domain.Entities.Common;

namespace AtiehJobCore.Domain.Entities.Employers
{
    /// <summary>
    /// نحوه آشنایی با مرکز
    /// </summary>
    public class IntroductionMethod : BaseEntity
    {
        public IntroductionMethod() {}

        public IntroductionMethod(string method)
        {
            Method = method;
        }
        public string Method { get; set; }
        /// <summary>
        /// ارتباط با کلاس کارفرما
        /// </summary>
        public virtual ICollection<Employer> Employers { get; set; }
    }
}
