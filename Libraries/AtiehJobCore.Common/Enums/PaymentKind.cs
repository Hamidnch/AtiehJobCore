using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.Common.Enums
{
    public enum PaymentKind
    {
        [Display(Name = "ثبت نام")]
        Enroll = 1,
        [Display(Name = "شارژ حساب")]
        Charge = 2,
        [Display(Name = "نامشخص")]
        Other = 3
    }
}
