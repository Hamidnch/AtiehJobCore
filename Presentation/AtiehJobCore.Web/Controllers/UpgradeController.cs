using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Installation;
using AtiehJobCore.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc;

namespace AtiehJobCore.Web.Controllers
{
    public partial class UpgradeController : BasePublicController
    {
        #region Fields

        private readonly IUpgradeService _upgradeService;
        #endregion

        #region Ctor

        public UpgradeController(IUpgradeService upgradeService)
        {
            _upgradeService = upgradeService;
        }
        #endregion

        public virtual IActionResult Index()
        {
            if (!DataSettingsHelper.DatabaseIsInstalled())
                return RedirectToRoute("Install");

            var model = new UpgradeModel
            {
                ApplicationVersion = SiteVersion.CurrentVersion,
                DatabaseVersion = _upgradeService.DatabaseVersion()
            };

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Index(UpgradeModel m)
        {
            var model = new UpgradeModel
            {
                ApplicationVersion = SiteVersion.CurrentVersion,
                DatabaseVersion = _upgradeService.DatabaseVersion()
            };

            if (model.ApplicationVersion != model.DatabaseVersion)
            {
                _upgradeService.UpgradeData(model.DatabaseVersion, model.ApplicationVersion);
            }

            //restart application
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            webHelper.RestartAppDomain();

            //Redirect to home page
            return RedirectToRoute("HomePage");

        }
    }
}
