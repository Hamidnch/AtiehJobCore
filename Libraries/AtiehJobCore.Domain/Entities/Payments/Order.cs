﻿using System;
using AtiehJobCore.Common.Enums;
using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.Identity;

namespace AtiehJobCore.Domain.Entities.Payments
{
    //کلاس سفارش برای پرداخت بانکی - تحت بررسی
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; }
        public int? PaymentGatewayCode { get; set; }
        public virtual PaymentGateway PaymentGateway { get; set; }
        public long? Amount { get; set; }
        public int? AmountUnitCode { get; set; }
        public virtual AmountUnit AmountUnit { get; set; }
        public DateTimeOffset? RequestDate { get; set; }
        public bool? RequestState { get; set; }
        public string ReferenceId { get; set; }
        public string PaymentNumber { get; set; }
        public PaymentKind? PaymentKind { get; set; }
        /***************** ارتباط با جدول کاربر ********************/
        /// <summary>
        /// شناسه کاربری
        /// </summary>
        public long UserId { get; set; }
        public virtual User User { get; set; }
        /***************** ارتباط با جدول کاربر ********************/
    }
}