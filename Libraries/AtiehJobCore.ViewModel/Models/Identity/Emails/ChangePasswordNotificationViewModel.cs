using AtiehJobCore.Domain.Entities.Identity;

namespace AtiehJobCore.ViewModel.Models.Identity.Emails
{
    public class ChangePasswordNotificationViewModel : EmailsBase
    {
        public User User { set; get; }
    }
}