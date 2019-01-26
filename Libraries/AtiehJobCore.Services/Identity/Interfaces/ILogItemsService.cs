using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AtiehJobCore.ViewModel.Models.Identity.Common;

namespace AtiehJobCore.Services.Identity.Interfaces
{
    public interface ILogItemsService
    {
        Task DeleteAllAsync(string logLevel = "");
        Task DeleteAsync(int logItemId);
        Task DeleteOlderThanAsync(DateTimeOffset cutoffDateUtc, string logLevel = "");
        Task<int> GetCountAsync(string logLevel = "");
        Task<PagedLogItemsViewModel> GetPagedLogItemsAsync(
            int pageNumber, int pageSize, SortOrder sortOrder, string logLevel = "");
    }
}