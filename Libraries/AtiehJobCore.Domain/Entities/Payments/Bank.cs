using System.Collections.Generic;
using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.Placements;

namespace AtiehJobCore.Domain.Entities.Payments
{
    /// <summary>
    /// بانک
    /// </summary>
    public class Bank : BaseEntity
    {
        public Bank() { }
        public Bank(string name, string branch)
        {
            Name = name;
            Branch = branch;
        }
        /// <summary>
        /// نام بانک
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// شعبه
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// ارتباط با کلاس حسابی کاریابی
        /// </summary>
        public virtual ICollection<PlacementBankAccount> PlacementBankAccounts { get; set; }
        /// <summary>
        /// ارتباط با کلاس درگاه های بانکی
        /// </summary>
        public virtual ICollection<PaymentGateway> PaymentGateways { get; set; }
    }
}
