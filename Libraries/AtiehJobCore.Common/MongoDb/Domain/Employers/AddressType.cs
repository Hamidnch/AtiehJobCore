using System.Collections.Generic;

namespace AtiehJobCore.Common.MongoDb.Domain.Employers
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
