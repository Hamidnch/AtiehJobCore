using AtiehJobCore.Common.Infrastructure.MongoDb;
using AtiehJobCore.Common.MongoDb.Domain.Users;
using AtiehJobCore.Services.MongoDb.Localization;
using AtiehJobCore.Services.MongoDb.Logging;
using AtiehJobCore.Services.MongoDb.Users;
using AtiehJobCore.Services.Security;
using AtiehJobCore.Web.Framework.Models.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtiehJobCore.Web.Areas.Admin.Controllers
{
    public class SecurityController : BaseAdminController
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionService;
        private readonly IUserService _customerService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Constructors

        public SecurityController(ILogger logger, IWorkContext workContext,
            IPermissionService permissionService,
            IUserService customerService, ILocalizationService localizationService)
        {
            this._logger = logger;
            this._workContext = workContext;
            this._permissionService = permissionService;
            this._customerService = customerService;
            this._localizationService = localizationService;
        }

        #endregion

        #region Methods

        public IActionResult AccessDenied(string pageUrl)
        {
            var currentUser = _workContext.CurrentUser;
            if (currentUser == null || currentUser.IsGuest())
            {
                _logger.Information($"Access denied to anonymous request on {pageUrl}");
                return View();
            }

            _logger.Information($"Access denied to user #{currentUser.Email} '{currentUser.Email}' on {pageUrl}");


            return View();
        }

        public IActionResult Permissions()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                return AccessDeniedView();

            var model = new PermissionMappingModel();

            var permissionRecords = _permissionService.GetAllPermissionRecords();
            var customerRoles = _customerService.GetAllUserRoles(true);
            foreach (var pr in permissionRecords)
            {
                model.AvailablePermissions.Add(new PermissionRecordModel
                {
                    //Name = pr.Name,
                    Name = pr.GetLocalizedPermissionName(_localizationService, _workContext),
                    SystemName = pr.SystemName
                });
            }
            foreach (var cr in customerRoles)
            {
                model.AvailableUserRoles.Add(new UserRoleModel() { Id = cr.Id, Name = cr.Name });
            }
            foreach (var pr in permissionRecords)
                foreach (var cr in customerRoles)
                {
                    var allowed = pr.UserRoles.Count(x => x == cr.Id) > 0;
                    if (!model.Allowed.ContainsKey(pr.SystemName))
                        model.Allowed[pr.SystemName] = new Dictionary<string, bool>();
                    model.Allowed[pr.SystemName][cr.Id] = allowed;
                }

            return View(model);
        }

        [HttpPost, ActionName("Permissions")]
        public IActionResult PermissionsSave(IFormCollection form)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                return AccessDeniedView();

            var permissionRecords = _permissionService.GetAllPermissionRecords();
            var customerRoles = _customerService.GetAllUserRoles(true);


            foreach (var cr in customerRoles)
            {
                var formKey = "allow_" + cr.Id;
                var permissionRecordSystemNamesToRestrict = form[formKey].ToString() != null ?
                    form[formKey].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList() : new List<string>();
                foreach (var pr in permissionRecords)
                {

                    var allow = permissionRecordSystemNamesToRestrict.Contains(pr.SystemName);
                    if (allow)
                    {
                        if (pr.UserRoles.FirstOrDefault(x => x == cr.Id) != null)
                        {
                            continue;
                        }

                        pr.UserRoles.Add(cr.Id);
                        _permissionService.UpdatePermissionRecord(pr);
                    }
                    else
                    {
                        if (pr.UserRoles.FirstOrDefault(x => x == cr.Id) == null)
                        {
                            continue;
                        }

                        pr.UserRoles.Remove(cr.Id);
                        _permissionService.UpdatePermissionRecord(pr);
                    }
                }
            }

            //SuccessNotification(_localizationService.GetResource("Admin.Configuration.ACL.Updated"));
            return RedirectToAction("Permissions");
        }

        #endregion
    }
}
