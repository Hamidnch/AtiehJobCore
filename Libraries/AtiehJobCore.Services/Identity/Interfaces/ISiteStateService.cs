using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AtiehJobCore.Domain.Entities.Identity;
using AtiehJobCore.ViewModel.Models.Identity.Common;

namespace AtiehJobCore.Services.Identity.Interfaces
{
    public interface ISiteStateService
    {
        Task<List<User>> GetOnlineUsersListAsync(int numbersToTake, int minutesToTake);

        Task<List<User>> GetTodayBirthdayListAsync();

        Task UpdateUserLastVisitDateTimeAsync(ClaimsPrincipal claimsPrincipal);

        Task<AgeStateViewModel> GetUsersAverageAge();
    }
}