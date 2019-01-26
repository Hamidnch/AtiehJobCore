using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AtiehJobCore.Common.Extensions;
using AtiehJobCore.Data.DbContext;
using AtiehJobCore.Domain.Entities.Identity.Plus;
using AtiehJobCore.Services.Identity.Interfaces;
using AtiehJobCore.ViewModel.Models.Identity.Common;
using Microsoft.EntityFrameworkCore;

namespace AtiehJobCore.Services.Identity.Logger
{
    public class LogItemsService : ILogItemsService
    {
        private readonly DbSet<LogItem> _logItems;
        private readonly IUnitOfWork _uow;
        public LogItemsService(IUnitOfWork uow)
        {
            _uow = uow;
            _uow.CheckArgumentIsNull(nameof(_uow));

            _logItems = _uow.Set<LogItem>();
        }

        public Task DeleteAllAsync(string logLevel = "")
        {
            if (string.IsNullOrWhiteSpace(logLevel))
            {
                _logItems.RemoveRange(_logItems);
            }
            else
            {
                var query = _logItems.Where(l => l.LogLevel == logLevel);
                _logItems.RemoveRange(query);
            }

            return _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(int logItemId)
        {
            var itemToRemove = await _logItems.FirstOrDefaultAsync(x => x.Id.Equals(logItemId));
            if (itemToRemove != null)
            {
                _logItems.Remove(itemToRemove);
                await _uow.SaveChangesAsync();
            }
        }

        public Task DeleteOlderThanAsync(DateTimeOffset cutOffDateUtc, string logLevel = "")
        {
            if (string.IsNullOrWhiteSpace(logLevel))
            {
                var query = _logItems.Where(l => l.CreatedDateTime < cutOffDateUtc);
                _logItems.RemoveRange(query);
            }
            else
            {
                var query = _logItems.Where(l => l.CreatedDateTime < cutOffDateUtc
                                                    && l.LogLevel == logLevel);
                _logItems.RemoveRange(query);
            }

            return _uow.SaveChangesAsync();
        }

        public Task<int> GetCountAsync(string logLevel = "")
        {
            return string.IsNullOrWhiteSpace(logLevel) ?
                            _logItems.CountAsync() :
                            _logItems.Where(l => l.LogLevel == logLevel).CountAsync();
        }

        public async Task<PagedLogItemsViewModel> GetPagedLogItemsAsync(
            int pageNumber, int pageSize, SortOrder sortOrder, string logLevel = "")
        {
            var offset = (pageSize * pageNumber) - pageSize;

            var query = string.IsNullOrWhiteSpace(logLevel) ? _logItems :
                             _logItems.Where(l => l.LogLevel == logLevel);

            query = sortOrder == SortOrder.Descending ?
                query.OrderByDescending(x => x.Id) : query.OrderBy(x => x.Id);

            return new PagedLogItemsViewModel
            {
                Paging =
                {
                    TotalItems = await query.CountAsync()
                },
                LogItems = await query.Skip(offset).Take(pageSize).ToListAsync()
            };
        }
    }
}