using AtiehJobCore.Common.MongoDb.Domain.Payments;

namespace AtiehJobCore.Common.MongoDb.Domain.Placements
{
    public class PlacementBankAccount : BaseMongoEntity
    {
        public PlacementBankAccount() { }

        public PlacementBankAccount(int bankCode, string accountNumber, int placementCode)
        {
            BankCode = bankCode;
            AccountNumber = accountNumber;
            PlacementCode = placementCode;
        }
        /// <summary>
        /// کد بانک
        /// </summary>
        public int BankCode { get; set; }
        public virtual Bank Bank { get; set; }
        /// <summary>
        /// شماره حساب
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// کد کاریاب
        /// </summary>
        public int PlacementCode { get; set; }
        /// <summary>
        /// ارتباط با کلاس کاریاب
        /// Navigation Property
        /// </summary>
        public virtual Placement Placement { get; set; }
    }
}
