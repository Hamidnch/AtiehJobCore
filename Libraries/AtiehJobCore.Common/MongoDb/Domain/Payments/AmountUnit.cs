using System.Collections.Generic;

namespace AtiehJobCore.Common.MongoDb.Domain.Payments
{
    /// <summary>
    /// کلاس واحد پولی
    /// </summary>
    public class AmountUnit : BaseMongoEntity
    {
        public AmountUnit() { }
        public AmountUnit(string title)
        {
            Title = title;
        }
        /// <summary>
        /// عنوان واحد پولی
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// کلاس ارتباط با جدول پرداخت
        /// </summary>
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
