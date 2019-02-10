using System.Collections.Generic;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Employers
{
    public class AddressType : BaseMongoEntity
    {
        /// <summary>
        /// نوع آدرس
        /// </summary>
        public AddressType() { }

        public AddressType(string type)
        {
            Type = type;
        }
        public string Type { get; set; }
        /// <summary>
        /// ارتباط با کلاس آدرس کارفرما
        /// </summary>
        public virtual ICollection<EmployerAddress> EmployerAddresses { get; set; }
    }
}
