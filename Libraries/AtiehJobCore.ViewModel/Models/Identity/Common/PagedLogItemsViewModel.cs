using System.Collections.Generic;
using AtiehJobCore.Domain.Entities.Identity.Plus;
using cloudscribe.Web.Pagination;

namespace AtiehJobCore.ViewModel.Models.Identity.Common
{
    public class PagedLogItemsViewModel
    {
        public PagedLogItemsViewModel()
        {
            Paging = new PaginationSettings();
        }

        public string LogLevel { get; set; } = string.Empty;

        public List<LogItem> LogItems { get; set; }

        public PaginationSettings Paging { get; set; }
    }
}