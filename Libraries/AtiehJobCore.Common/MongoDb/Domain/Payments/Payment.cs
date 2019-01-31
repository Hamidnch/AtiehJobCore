using System;
using System.Collections.Generic;
using AtiehJobCore.Common.Enums;
using AtiehJobCore.Common.MongoDb.Domain.Users;

namespace AtiehJobCore.Common.MongoDb.Domain.Payments
{
    /// <summary>
    /// کلاس پرداخت ها
    /// </summary>
    public class Payment : BaseMongoEntity
    {
        public Payment() { }

        public Payment(long userId, string userIp, int? paymentGatewayCode, long? amount,
            int? amountUnitCode, DateTimeOffset? requestDate, bool? requestState,
            DateTimeOffset? paymentDate, bool? paymentState, DateTimeOffset? verifyDate,
            bool? verifyState, string orderId, string referenceId, string paymentNumber)
        {
            UserId = userId;
            UserIp = userIp;
            PaymentGatewayCode = paymentGatewayCode;
            Amount = amount;
            AmountUnitCode = amountUnitCode;
            RequestDate = requestDate;
            RequestState = requestState;
            PaymentDate = paymentDate;
            PaymentState = paymentState;
            VerifyDate = verifyDate;
            VerifyState = verifyState;
            OrderId = orderId;
            ReferenceId = referenceId;
            PaymentNumber = paymentNumber;
        }
        public string UserIp { get; set; }
        public int? PaymentGatewayCode { get; set; }
        public virtual PaymentGateway PaymentGateway { get; set; }
        public long? Amount { get; set; }
        public int? AmountUnitCode { get; set; }
        public virtual AmountUnit AmountUnit { get; set; }
        public DateTimeOffset? RequestDate { get; set; }
        public bool? RequestState { get; set; }
        public DateTimeOffset? PaymentDate { get; set; }
        public bool? PaymentState { get; set; }
        public DateTimeOffset? VerifyDate { get; set; }
        public bool? VerifyState { get; set; }
        public string OrderId { get; set; }
        public string ReferenceId { get; set; }
        public string PaymentNumber { get; set; }
        public string Token { get; set; }
        public PaymentKind? PaymentKind { get; set; }
        public virtual ICollection<UserAccountCharge> UserAccountCharges { get; set; }
        /***************** ارتباط با جدول کاربر ********************/
        /// <summary>
        /// شناسه کاربری
        /// </summary>
        public long UserId { get; set; }
        public virtual User User { get; set; }
        /***************** ارتباط با جدول کاربر ********************/
    }
}
