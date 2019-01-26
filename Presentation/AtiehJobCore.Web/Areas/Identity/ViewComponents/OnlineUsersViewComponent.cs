using AtiehJobCore.Services.Identity.Interfaces;
using AtiehJobCore.ViewModel.Models.Identity.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AtiehJobCore.Web.Areas.Identity.ViewComponents
{
    public class OnlineUsersViewComponent : ViewComponent
    {
        private readonly ISiteStateService _siteStateService;

        public OnlineUsersViewComponent(ISiteStateService siteStateService)
        {
            _siteStateService = siteStateService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int numbersToTake, int minutesToTake, bool showMoreItemsLink)
        {
            var usersList = await _siteStateService.GetOnlineUsersListAsync(numbersToTake, minutesToTake);
            return View(viewName: "~/Areas/Identity/Views/Shared/Components/OnlineUsers/Default.cshtml",
                model: new OnlineUsersViewModel
                {
                    MinutesToTake = minutesToTake,
                    NumbersToTake = numbersToTake,
                    ShowMoreItemsLink = showMoreItemsLink,
                    Users = usersList
                });
        }
    }
}
