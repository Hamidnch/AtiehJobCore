using System.Collections.Generic;
using AtiehJobCore.Domain.Entities.Identity;
using cloudscribe.Web.Pagination;

namespace AtiehJobCore.ViewModel.Models.Identity.Account
{
    public class PagedUsersListViewModel
    {
        public PagedUsersListViewModel()
        {
            Paging = new PaginationSettings();
        }

        public List<User> Users { get; set; }

        public List<Role> Roles { get; set; }

        public PaginationSettings Paging { get; set; }
    }
}
