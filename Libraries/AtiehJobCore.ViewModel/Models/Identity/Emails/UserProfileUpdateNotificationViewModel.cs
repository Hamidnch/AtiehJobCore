using AtiehJobCore.Domain.Entities.Identity;

namespace AtiehJobCore.ViewModel.Models.Identity.Emails
{
    public class UserProfileUpdateNotificationViewModel : EmailsBase
    {
        public User User { set; get; }
    }
}