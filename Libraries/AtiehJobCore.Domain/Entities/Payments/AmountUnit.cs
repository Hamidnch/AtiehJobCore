using System.Collections.Generic;
using AtiehJobCore.Domain.Entities.Common;

namespace AtiehJobCore.Domain.Entities.Payments
{
    /// <summary>
    /// کلاس واحد پولی
    /// </summary>
    public class AmountUnit : BaseEntity
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
