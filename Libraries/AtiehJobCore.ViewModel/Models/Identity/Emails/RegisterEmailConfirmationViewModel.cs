using AtiehJobCore.Domain.Entities.Identity;

namespace AtiehJobCore.ViewModel.Models.Identity.Emails
{
    public class RegisterEmailConfirmationViewModel : EmailsBase
    {
        public User User { set; get; }
        public string EmailConfirmationToken { set; get; }
    }
}
