using AtiehJobCore.Common.MongoDb.Domain.Users;

namespace AtiehJobCore.Common.MongoDb.Domain.Payments
{
    /// <summary>
    /// کلاس شارژ اکانت تعدادی
    /// </summary>
    public class UserAccountCharge : BaseMongoEntity
    {
        public UserAccountCharge() { }

        public UserAccountCharge(int userId, long chargeAmountBase, int chargeCount)
        {
            UserId = userId;
            BaseChargeAmount = chargeAmountBase;
            ChargeCount = chargeCount;
        }
        /// <summary>
        /// کاربر شارژ کننده حساب
        /// </summary>
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string OrderId { get; set; }
        /// <summary>
        /// مبلغ پایه برای رزرو هر رکورد که از بخش مدیریت سایت قابل تنظیم است
        /// </summary>
        public long BaseChargeAmount { get; set; }
        /// <summary>
        /// تعداد شارژ برای رزرو
        /// </summary>
        public int ChargeCount { get; set; }

        public int? PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
