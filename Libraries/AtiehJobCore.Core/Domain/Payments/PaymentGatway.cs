using System.Collections.Generic;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Payments
{
    /// <summary>
    /// کلاس درگاه بانکی
    /// </summary>
    public class PaymentGateway : BaseMongoEntity
    {
        /// <summary>
        /// نام درگاه بانکی
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// کد بانک
        /// </summary>
        public int BankCode { get; set; }
        public virtual Bank Bank { get; set; }

        public virtual ICollection<PaymentSetting> PaymentSettings { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
