using System;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Payments
{
    /// <summary>
    /// کلاس صندوق حساب کاربران
    /// </summary>
    public class UserChargeBox : BaseMongoEntity
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
