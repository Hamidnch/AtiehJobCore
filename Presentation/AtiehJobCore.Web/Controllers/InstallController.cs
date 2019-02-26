using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Configuration;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Core.Plugins;
using AtiehJobCore.Core.Utilities;
using AtiehJobCore.Services.Installation;
using AtiehJobCore.Services.Logging;
using AtiehJobCore.Services.Security;
using AtiehJobCore.Web.Framework.Models;
using AtiehJobCore.Web.Framework.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace AtiehJobCore.Web.Controllers
{
    public class InstallController : Controller
    {
        #region Fields

        private readonly IInstallationLocalizationService _locService;
        private readonly AtiehJobConfig _config;
        private readonly ICacheManager _cacheManager;
        private readonly IHttpContextAccessor _contextAccessor;

        #endregion

        #region Ctor

        public InstallController(IInstallationLocalizationService locService,
            AtiehJobConfig config, ICacheManager cacheManager, IHttpContextAccessor contextAccessor)
        {
            _locService = locService;
            _config = config;
            _cacheManager = cacheManager;
            _contextAccessor = contextAccessor;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// A value indicating whether we use MARS (Multiple Active Result Sets)
        /// </summary>
        protected bool UseMars => false;

        #endregion

        #region Methods

        public virtual IActionResult Index()
        {
            if (DataSettingsHelper.DatabaseIsInstalled())
                return RedirectToAction("Index", "Home");

            var model = new InstallModel
            {
                AdminEmail = "admin@gmail.com",
                InstallSampleData = false,
                DatabaseConnectionString = "",
                DataProvider = "mongodb"

            };
            foreach (var lang in _locService.GetAvailableLanguages())
            {
                model.AvailableLanguages.Add(new SelectListItem
                {
                    Value = Url.Action("ChangeLanguage", "Install", new { language = lang.Code }),
                    Text = lang.Name,
                    Selected = _locService.GetCurrentLanguage().Code == lang.Code,
                });
            }
            //prepare collation list
            foreach (var col in _locService.GetAvailableCollations())
            {
                model.AvailableCollation.Add(new SelectListItem
                {
                    Value = col.Value,
                    Text = col.Name,
                    Selected = _locService.GetCurrentLanguage().Code == col.Value,
                });
            }

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Index(InstallModel model)
        {
            if (DataSettingsHelper.DatabaseIsInstalled())
                return RedirectToAction("Index", "Home");

            if (model.DatabaseConnectionString != null)
                model.DatabaseConnectionString = model.DatabaseConnectionString.Trim();

            var connectionString = "";

            if (model.MongoDbConnectionInfo)
            {
                if (string.IsNullOrEmpty(model.DatabaseConnectionString))
                {
                    ModelState.AddModelError("", _locService.GetResource("ConnectionStringRequired"));
                }
                else
                {
                    connectionString = model.DatabaseConnectionString;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(model.MongoDbDatabaseName))
                {
                    ModelState.AddModelError("", _locService.GetResource("DatabaseNameRequired"));
                }
                if (string.IsNullOrEmpty(model.MongoDbServerName))
                {
                    ModelState.AddModelError("", _locService.GetResource("MongoDBServerNameRequired"));
                }
                var userNameAndPassword = "";
                if (!(string.IsNullOrEmpty(model.MongoDbUsername)))
                {
                    userNameAndPassword = model.MongoDbUsername + ":" + model.MongoDbPassword + "@";
                }

                connectionString = "mongodb://" + userNameAndPassword + model.MongoDbServerName + "/" + model.MongoDbDatabaseName;
            }

            if (!string.IsNullOrEmpty(connectionString))
            {
                try
                {
                    var client = new MongoClient(connectionString);
                    var databaseName = new MongoUrl(connectionString).DatabaseName;
                    var database = client.GetDatabase(databaseName);
                    database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait();

                    var filter = new BsonDocument("name", "AtiehJobVersion");
                    var found = database.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter }).Result;

                    if (found.Any())
                        ModelState.AddModelError("", _locService.GetResource("AlreadyInstalled"));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                }
            }
            else
                ModelState.AddModelError("", _locService.GetResource("ConnectionStringRequired"));

            var webHelper = EngineContext.Current.Resolve<IWebHelper>();

            //validate permissions
            var dirsToCheck = FilePermissionHelper.GetDirectoriesWrite();
            foreach (var dir in dirsToCheck)
                if (!FilePermissionHelper.CheckPermissions(dir, false, true, true, false))
                    ModelState.AddModelError("", string.Format(_locService.GetResource(
                        "ConfigureDirectoryPermissions"), WindowsIdentity.GetCurrent().Name, dir));

            var filesToCheck = FilePermissionHelper.GetFilesWrite();
            foreach (var file in filesToCheck)
                if (!FilePermissionHelper.CheckPermissions(file, false, true, true, true))
                    ModelState.AddModelError("", string.Format(_locService.GetResource(
                        "ConfigureFilePermissions"), WindowsIdentity.GetCurrent().Name, file));

            if (ModelState.IsValid)
            {
                var settingsManager = new DataSettingsManager();
                try
                {
                    //save settings
                    var settings = new DataSettings
                    {
                        DataProvider = "mongodb",
                        DataConnectionString = connectionString
                    };
                    settingsManager.SaveSettings(settings);

                    var dataProviderInstance =
                        EngineContext.Current.Resolve<BaseDataProviderManager>().LoadDataProvider();
                    dataProviderInstance.InitDatabase();

                    var dataSettingsManager = new DataSettingsManager();
                    var dataProviderSettings = dataSettingsManager.LoadSettings(reloadSettings: true);

                    var installationService = EngineContext.Current.Resolve<IInstallationService>();
                    installationService.InstallData(
                        model.AdminEmail, model.AdminPassword, model.Collation, model.InstallSampleData);

                    //reset cache
                    DataSettingsHelper.ResetCache();

                    //install plugins
                    PluginManager.MarkAllPluginsAsUninstalled();
                    var pluginFinder = EngineContext.Current.Resolve<IPluginFinder>();
                    var plugins = pluginFinder.GetPlugins<IPlugin>(LoadPluginsMode.All)
                        .ToList()
                        .OrderBy(x => x.PluginDescriptor.Group)
                        .ThenBy(x => x.PluginDescriptor.DisplayOrder)
                        .ToList();

                    var pluginsIgnoredDuringInstallation = string.IsNullOrEmpty(_config.PluginsIgnoredDuringInstallation) ?
                        new List<string>() :
                        _config.PluginsIgnoredDuringInstallation
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim())
                        .ToList();

                    foreach (var plugin in plugins)
                    {
                        if (pluginsIgnoredDuringInstallation.Contains(plugin.PluginDescriptor.SystemName))
                            continue;

                        try
                        {
                            plugin.Install();
                        }
                        catch (Exception ex)
                        {
                            var logger = EngineContext.Current.Resolve<ILogger>();
                            logger.InsertLog(MongoLogLevel.Error,
                                "Error during installing plugin " + plugin.PluginDescriptor.SystemName,
                                ex.Message + " " + ex.InnerException?.Message);
                        }
                    }

                    //register default permissions
                    var permissionProviders = new List<Type>
                    {
                        typeof(StandardPermissionProvider)
                    };
                    foreach (var providerType in permissionProviders)
                    {
                        var provider = (IPermissionProvider)Activator.CreateInstance(providerType);
                        EngineContext.Current.Resolve<IPermissionService>().InstallPermissions(provider);
                    }

                    //restart application
                    if (Core.Infrastructure.OperatingSystem.IsWindows())
                    {
                        webHelper.RestartAppDomain();
                        //Redirect to home page
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View(new InstallModel() { Installed = true });
                    }
                }
                catch (Exception exception)
                {
                    //reset cache
                    DataSettingsHelper.ResetCache();
                    _cacheManager.Clear();

                    System.IO.File.Delete(CommonHelper.MapPath("~/App_Data/Settings.txt"));

                    ModelState.AddModelError("", string.Format(_locService.GetResource("SetupFailed"), exception.Message + " " + exception.InnerException?.Message));
                }
            }

            //prepare language list
            foreach (var lang in _locService.GetAvailableLanguages())
            {
                model.AvailableLanguages.Add(new SelectListItem
                {
                    Value = Url.Action("ChangeLanguage", "Install", new { language = lang.Code }),
                    Text = lang.Name,
                    Selected = _locService.GetCurrentLanguage().Code == lang.Code,
                });
            }

            //prepare collation list
            foreach (var col in _locService.GetAvailableCollations())
            {
                model.AvailableCollation.Add(new SelectListItem
                {
                    Value = col.Value,
                    Text = col.Name,
                    Selected = _locService.GetCurrentLanguage().Code == col.Value,
                });
            }

            return View(model);
        }

        public virtual IActionResult ChangeLanguage(string language)
        {
            if (DataSettingsHelper.DatabaseIsInstalled())
                return RedirectToAction("Index", "Home");

            _locService.SaveCurrentLanguage(language);

            //Reload the page
            return RedirectToAction("Index", "Install");
        }

        public virtual IActionResult RestartInstall()
        {
            if (DataSettingsHelper.DatabaseIsInstalled())
                return RedirectToAction("Index", "Home");

            //restart application
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            webHelper.RestartAppDomain();

            //Redirect to home page
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
