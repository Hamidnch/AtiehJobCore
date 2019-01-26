using System.Data.SqlClient;
using System.Threading.Tasks;
using AtiehJobCore.Common.Constants;
using AtiehJobCore.Common.Extensions;
using AtiehJobCore.Domain.Entities.Identity;
using AtiehJobCore.Services.Constants;
using AtiehJobCore.Services.Identity.Interfaces;
using AtiehJobCore.ViewModel.Models.Identity.Account;
using AtiehJobCore.ViewModel.Models.Identity.Common;
using DNTBreadCrumb.Core;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtiehJobCore.Web.Areas.Identity.Controllers
{
    [Area(AreaNames.Identity)]
    [Authorize(Roles = RoleNames.Admin)]
    [BreadCrumb(Title = "مدیریت نقش‌ها", UseDefaultRouteUrl = true, Order = 0)]
    public class RolesManagerController : Controller
    {
        private const string RoleNotFound = "نقش درخواستی یافت نشد.";
        private const int DefaultPageSize = 7;
        private readonly IRoleManager _roleManager;

        public RolesManagerController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
            _roleManager.CheckArgumentIsNull(nameof(_roleManager));
        }

        public IActionResult Index()
        {
            var roles = _roleManager.GetAllRolesAndUsersCountList();
            return View(roles);
        }

        [AjaxOnly]
        public async Task<IActionResult> RenderRole([FromBody]ModelIdViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.Id))
            {
                return PartialView("_Create");
            }

            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role != null)
            {
                return PartialView("_Create", model: new RoleViewModel { Id = role.Id.ToString(), Name = role.Name });
            }

            ModelState.AddModelError("", RoleNotFound);
            return PartialView("_Create");
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
                if (role == null)
                    ModelState.AddModelError("", RoleNotFound);
                else
                {
                    role.Name = model.Name;
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return Json(new { success = true });
                    }
                    ModelState.AddErrorsFromIdentityResult(result);
                }
            }
            return PartialView("_Create", model: model);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Create", model: model);
            }

            var result = await _roleManager.CreateAsync(new Role(model.Name));
            if (result.Succeeded)
            {
                return Json(new { success = true });
            }
            ModelState.AddErrorsFromIdentityResult(result);
            return PartialView("_Create", model: model);
        }

        [AjaxOnly]
        public async Task<IActionResult> RenderDeleteRole([FromBody]ModelIdViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.Id))
            {
                return PartialView("_Delete");
            }

            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role != null)
            {
                return PartialView("_Delete", model: new RoleViewModel { Id = role.Id.ToString(), Name = role.Name });
            }

            ModelState.AddModelError("", RoleNotFound);
            return PartialView("_Delete");
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ModelState.AddModelError("", RoleNotFound);
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
                ModelState.AddErrorsFromIdentityResult(result);
            }
            return PartialView("_Delete", model: model);
        }

        [BreadCrumb(Title = "لیست کاربران دارای نقش", Order = 1)]
        public async Task<IActionResult> UsersInRole(int? id, int? page = 1, string field = "Id", SortOrder order = SortOrder.Ascending)
        {
            if (id == null)
            {
                return View("Error");
            }

            var pageNumber = 1;
            if (page != null) pageNumber = page.Value;

            var model = await _roleManager.GetPagedUsersInRoleListAsync(
                roleId: id.Value,
                pageNumber: pageNumber - 1,
                recordsPerPage: DefaultPageSize,
                sortByField: field,
                sortOrder: order,
                showAllUsers: true);

            model.Paging.CurrentPage = pageNumber;
            model.Paging.ItemsPerPage = DefaultPageSize;
            model.Paging.ShowFirstLast = true;

            if (HttpContext.Request.IsAjaxRequest())
            {
                return PartialView("~/Areas/Identity/Views/UsersManager/_UsersList.cshtml", model);
            }
            return View(model);
        }
    }
}
