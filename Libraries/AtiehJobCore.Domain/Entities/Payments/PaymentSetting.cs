using AtiehJobCore.Domain.Entities.Common;

namespace AtiehJobCore.Domain.Entities.Payments
{
    public class PaymentSetting : BaseEntity
    {
        /// <summary>
        /// شماره پایانه
        /// </summary>
        public string TerminalId { get; set; }
        /// <summary>
        /// کد پذیرنده
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// کد کاربری پذیرنده
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// پسورد کاربری پذیرنده
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// کد Sha1Key
        /// </summary>
        public string Sha1Key { get; set; }
        /// <summary>
        /// آی پی معتبر سایت پذیرنده
        /// </summary>
        public string SiteIp { get; set; }
        /// <summary>
        /// آدرس برگشت از پرداخت از طریق درگاه بانک
        /// </summary>
        public string CallbackUrl { get; set; }
        /// <summary>
        /// آدرس صفحه پرداخت بانک
        /// </summary>
        public string BankUrl { get; set; }
        /// <summary>
        /// شناسه درگاه بانکی
        /// </summary>
        public int PaymentGatewayId { get; set; }
        public virtual PaymentGateway PaymentGateway { get; set; }
    }
}
