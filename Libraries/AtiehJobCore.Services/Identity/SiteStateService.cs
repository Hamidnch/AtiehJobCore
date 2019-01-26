using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AtiehJobCore.Common.Extensions;
using AtiehJobCore.Data.DbContext;
using AtiehJobCore.Domain.Entities.Identity;
using AtiehJobCore.Services.Identity.Interfaces;
using AtiehJobCore.ViewModel.Models.Identity.Common;
using DNTPersianUtils.Core;
using Microsoft.EntityFrameworkCore;

namespace AtiehJobCore.Services.Identity
{
    public class SiteStateService : ISiteStateService
    {
        private readonly IUserManager _userManager;
        private readonly DbSet<User> _users;

        public SiteStateService(IUserManager userManager, IUnitOfWork uow)
        {
            _userManager = userManager;
            _userManager.CheckArgumentIsNull(nameof(_userManager));

            //var uow1 = uow;
            uow.CheckArgumentIsNull(nameof(uow));

            _users = uow.Set<User>();
        }

        public Task<List<User>> GetOnlineUsersListAsync(int numbersToTake, int minutesToTake)
        {
            var now = DateTimeOffset.UtcNow;
            var minutes = now.AddMinutes(-minutesToTake);
            return _users.AsNoTracking()
                         .Where(user => user.LastVisitedDateTime != null
                                        && user.LastVisitedDateTime.Value <= now
                                        && user.LastVisitedDateTime.Value >= minutes)
                         .OrderByDescending(user => user.LastVisitedDateTime)
                         .Take(numbersToTake)
                         .ToListAsync();
        }

        public Task<List<User>> GetTodayBirthdayListAsync()
        {
            var now = DateTimeOffset.UtcNow;
            var day = now.Day;
            var month = now.Month;
            return _users.AsNoTracking()
                         .Where(user => user.DateOfBirth.HasValue && user.IsActive &&
                                user.DateOfBirth.Value.Day == day && user.DateOfBirth.Value.Month == month)
                         .ToListAsync();
        }

        public async Task<AgeStateViewModel> GetUsersAverageAge()
        {
            var users = await _users.AsNoTracking()
                                    .Where(x => x.DateOfBirth.HasValue && x.IsActive)
                                    .OrderBy(x => x.DateOfBirth)
                                    .ToListAsync()
                                    ;

            var count = users.Count;
            if (count == 0)
            {
                return new AgeStateViewModel();
            }

            var sum = users.Where(user => user.DateOfBirth != null)
                          .Sum(user => (int?)user.DateOfBirth.Value.GetAge()) ?? 0;

            return new AgeStateViewModel
            {
                AverageAge = sum / count,
                MaxAgeUser = users.First(),
                MinAgeUser = users.Last(),
                UsersCount = count
            };
        }

        public async Task UpdateUserLastVisitDateTimeAsync(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            user.LastVisitedDateTime = DateTimeOffset.UtcNow;
            await _userManager.UpdateAsync(user);
        }
    }
}