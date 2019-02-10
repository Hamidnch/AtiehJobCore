using System.Collections.Generic;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Employers
{
    /// <summary>
    /// نحوه آشنایی با مرکز
    /// </summary>
    public class IntroductionMethod : BaseMongoEntity
    {
        public IntroductionMethod() { }

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
