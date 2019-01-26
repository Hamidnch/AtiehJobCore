using System.Collections.Generic;
using AtiehJobCore.Domain.Entities.Identity;

namespace AtiehJobCore.ViewModel.Models.Identity.Common
{
    public class OnlineUsersViewModel
    {
        public List<User> Users { set; get; }
        public int NumbersToTake { set; get; }
        public int MinutesToTake { set; get; }
        public bool ShowMoreItemsLink { set; get; }
    }
}