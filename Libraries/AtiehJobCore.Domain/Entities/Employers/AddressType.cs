using System.Collections.Generic;
using AtiehJobCore.Domain.Entities.Common;

namespace AtiehJobCore.Domain.Entities.Employers
{
    public class AddressType : BaseEntity
    {
        /// <summary>
        /// نوع آدرس
        /// </summary>
        public AddressType() {}

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
