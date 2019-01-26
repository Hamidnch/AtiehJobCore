using AtiehJobCore.Domain.Entities.Identity;
using DNTPersianUtils.Core;

namespace AtiehJobCore.ViewModel.Models.Identity.Common
{
    public class AgeStateViewModel
    {
        private const char RleChar = (char)0x202B;

        public int UsersCount { set; get; }
        public int AverageAge { set; get; }
        public User MaxAgeUser { set; get; }
        public User MinAgeUser { set; get; }

        public string MinMax => $"{RleChar}جوان‌ترین عضو: {MinAgeUser.DisplayName} " +
                                $"({MinAgeUser.DateOfBirth.Value.GetAge()})، مسن‌ترین عضو: " +
                                $"{MaxAgeUser.DisplayName} ({MaxAgeUser.DateOfBirth.Value.GetAge()})، در بین {UsersCount} نفر";
    }
}