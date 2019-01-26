using System;
using AtiehJobCore.Domain.Entities.Common;
using AtiehJobCore.Domain.Entities.Identity;

namespace AtiehJobCore.Domain.Entities.Payments
{
    /// <summary>
    /// کلاس صندوق حساب کاربران
    /// </summary>
    public class UserChargeBox : BaseEntity
    {
        public UserChargeBox()
        {
        }

        public UserChargeBox(long userId, long totalAmount, long? registerUserId,
            DateTimeOffset? registerDate, bool? status, string description)
        {
            UserId = userId;
            TotalAmount = totalAmount;
            //RegisterUserId = registerUserId;
            //RegisterDate = registerDate;
            //Status = status;
            Description = description;
        }

        public long UserId { get; set; }
        public virtual User User { get; set; }
        public long TotalAmount { get; set; }
    }
}
