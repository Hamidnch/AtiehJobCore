using System.Collections.Generic;
using AtiehJobCore.Domain.Entities.Identity;

namespace AtiehJobCore.ViewModel.Models.Identity.Common
{
    public class TodayBirthDaysViewModel
    {
        public List<User> Users { set; get; }

        public AgeStateViewModel AgeState { set; get; }
    }
}