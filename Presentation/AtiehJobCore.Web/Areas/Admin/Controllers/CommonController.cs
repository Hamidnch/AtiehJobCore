using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Configuration;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Catalog;
using AtiehJobCore.Core.Domain.Logging;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Core.Plugins;
using AtiehJobCore.Core.Roslyn;
using AtiehJobCore.Data.Context;
using AtiehJobCore.Services.Helpers;
using AtiehJobCore.Services.Infrastructure;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Services.Security;
using AtiehJobCore.Services.Seo;
using AtiehJobCore.Services.Users;
using AtiehJobCore.Web.Framework.KendoUi;
using AtiehJobCore.Web.Framework.Models.Admin;
using AtiehJobCore.Web.Framework.Security;
using AtiehJobCore.Web.Framework.Security.Authorization;
using AtiehJobCore.Web.Framework.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Core.Bindings;
using MongoDB.Driver.Core.Operations;

namespace AtiehJobCore.Web.Areas.Admin.Controllers
{
    [PermissionAuthorize(PermissionSystemName.Maintenance)]
    public partial class CommonController : BaseAdminController
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IWebHelper _webHelper;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly CatalogSettings _catalogSettings;
        private readonly AtiehJobConfig _atiehJobConfig;
        private readonly IMongoDbContext _mongoDbContext;

        #endregion

        #region Constructors

        public CommonController(
            IUserService userService,
            IUrlRecordService urlRecordService,
            IWebHelper webHelper,
            IDateTimeHelper dateTimeHelper,
            ILanguageService languageService,
            ILocalizationService localizationService,
            CatalogSettings catalogSettings,
            AtiehJobConfig atiehJobConfig,
            IMongoDbContext mongoDbContext
            )
        {
            _userService = userService;
            _urlRecordService = urlRecordService;
            _webHelper = webHelper;
            _dateTimeHelper = dateTimeHelper;
            _languageService = languageService;
            _localizationService = localizationService;
            _catalogSettings = catalogSettings;
            _atiehJobConfig = atiehJobConfig;
            _mongoDbContext = mongoDbContext;
        }

        #endregion

        protected virtual IEnumerable<Dictionary<string, object>> Serialize(List<BsonValue> collection)
        {
            var results = new List<Dictionary<string, object>>();
            var document = collection.FirstOrDefault()?.AsBsonDocument;
            var cols = new List<string>();
            if (document == null)
            {
                return results;
            }

            var columns = document.Names.ToList();
            foreach (var row in collection)
            {
                var myObject = new Dictionary<string, object>();
                foreach (var col in columns)
                {
                    myObject.Add(col, row[col].ToString());
                }
                results.Add(myObject);
            }
            return results;
        }

        #region Methods

        public IActionResult SystemInfo()
        {
            var model = new SystemInfoModel
            {
                SiteVersion = SiteVersion.CurrentVersion
            };
            try
            {
                model.OperatingSystem = RuntimeInformation.OSDescription;
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                model.AspNetInfo = RuntimeEnvironment.GetSystemVersion();
            }
            catch (Exception)
            {
                // ignored
            }

            var machineNameProvider = EngineContext.Current.Resolve<IMachineNameProvider>();
            model.MachineName = machineNameProvider.GetMachineName();

            model.ServerTimeZone = TimeZoneInfo.Local.StandardName;
            model.ServerLocalTime = DateTime.Now;
            model.UtcTime = DateTime.UtcNow;
            foreach (var header in HttpContext.Request.Headers)
            {
                model.ServerVariables.Add(new SystemInfoModel.ServerVariableModel
                {
                    Name = header.Key,
                    Value = header.Value
                });
            }
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                model.LoadedAssemblies.Add(new SystemInfoModel.LoadedAssembly
                {
                    FullName = assembly.FullName,
                });
            }
            return View(model);
        }


        public IActionResult Warnings()
        {
            var model = new List<SystemWarningModel>();

            //incompatible plugins
            if (PluginManager.IncompatiblePlugins != null)
                model.AddRange(PluginManager.IncompatiblePlugins.Select(pluginName => new SystemWarningModel
                {
                    Level = SystemWarningLevel.Warning,
                    Text = string.Format(_localizationService.GetResource("Admin.System.Warnings.IncompatiblePlugin"), pluginName)
                }));

            if (!_catalogSettings.IgnoreAcl)
            {
                model.Add(new SystemWarningModel
                {
                    Level = SystemWarningLevel.Warning,
                    Text = _localizationService.GetResource("Admin.System.Warnings.Performance.IgnoreAcl")
                });
            }

            //validate write permissions (the same procedure like during installation)
            var dirPermissionsOk = true;
            var dirsToCheck = FilePermissionHelper.GetDirectoriesWrite();
            foreach (var dir in dirsToCheck)
                if (!FilePermissionHelper.CheckPermissions(dir, false, true, true, false))
                {
                    model.Add(new SystemWarningModel
                    {
                        Level = SystemWarningLevel.Warning,
                        Text = string.Format(_localizationService.GetResource("Admin.System.Warnings.DirectoryPermission.Wrong"),
                            WindowsIdentity.GetCurrent().Name, dir)
                    });
                    dirPermissionsOk = false;
                }
            if (dirPermissionsOk)
                model.Add(new SystemWarningModel
                {
                    Level = SystemWarningLevel.Pass,
                    Text = _localizationService.GetResource("Admin.System.Warnings.DirectoryPermission.OK")
                });

            var filePermissionsOk = true;
            var filesToCheck = FilePermissionHelper.GetFilesWrite();
            foreach (var file in filesToCheck)
                if (!FilePermissionHelper.CheckPermissions(file, false, true, true, true))
                {
                    model.Add(new SystemWarningModel
                    {
                        Level = SystemWarningLevel.Warning,
                        Text = string.Format(_localizationService.GetResource("Admin.System.Warnings.FilePermission.Wrong"),
                            WindowsIdentity.GetCurrent().Name, file)
                    });
                    filePermissionsOk = false;
                }
            if (filePermissionsOk)
            {
                model.Add(new SystemWarningModel
                {
                    Level = SystemWarningLevel.Pass,
                    Text = _localizationService.GetResource("Admin.System.Warnings.FilePermission.OK")
                });
            }
            return View(model);
        }


        public IActionResult Maintenance()
        {
            var model = new MaintenanceModel
            {
                DeleteGuests =
                {
                    EndDate = DateTime.UtcNow.AddDays(-7)
                }
            };
            return View(model);
        }
        [HttpPost, ActionName("Maintenance")]
        [FormValueRequired("delete-guests")]
        public IActionResult MaintenanceDeleteGuests(MaintenanceModel model)
        {
            var startDateValue = (model.DeleteGuests.StartDate == null) ? null
                            : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.DeleteGuests.StartDate.Value, _dateTimeHelper.CurrentTimeZone);

            var endDateValue = (model.DeleteGuests.EndDate == null) ? null
                            : (DateTime?)_dateTimeHelper
                            .ConvertToUtcTime(model.DeleteGuests.EndDate.Value, _dateTimeHelper.CurrentTimeZone).AddDays(1);

            model.DeleteGuests.NumberOfDeletedUsers = _userService.DeleteGuestUsers(startDateValue, endDateValue);

            return View(model);
        }

        [HttpPost, ActionName("Maintenance")]
        [FormValueRequired("delete-exported-files")]
        public IActionResult MaintenanceDeleteFiles(MaintenanceModel model)
        {
            var startDateValue = (model.DeleteExportedFiles.StartDate == null) ? null
                            : (DateTime?)_dateTimeHelper
                   .ConvertToUtcTime(model.DeleteExportedFiles.StartDate.Value, _dateTimeHelper.CurrentTimeZone);

            var endDateValue = (model.DeleteExportedFiles.EndDate == null) ? null
                            : (DateTime?)_dateTimeHelper
                   .ConvertToUtcTime(model.DeleteExportedFiles.EndDate.Value, _dateTimeHelper.CurrentTimeZone).AddDays(1);

            model.DeleteExportedFiles.NumberOfDeletedFiles = 0;
            return View(model);
        }


        [HttpPost, ActionName("Maintenance")]
        [FormValueRequired("delete-activitylog")]
        public IActionResult MaintenanceDeleteActivityLog(MaintenanceModel model)
        {
            var activityLogRepository = EngineContext.Current.Resolve<IRepository<ActivityLog>>();
            activityLogRepository.Collection.DeleteMany(new BsonDocument());
            model.DeleteActivityLog = true;
            return View(model);
        }
        public IActionResult ClearCache(string returnUrl = "")
        {
            var cacheManager = EngineContext.Current.Resolve<ICacheManager>();
            cacheManager.Clear();

            //home page
            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            //prevent open redirection attack
            if (!Url.IsLocalUrl(returnUrl))
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            return Redirect(returnUrl);
        }


        public IActionResult RestartApplication(string returnUrl = "")
        {
            //restart application
            _webHelper.RestartAppDomain();

            //home page
            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            //prevent open redirection attack
            if (!Url.IsLocalUrl(returnUrl))
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            return Redirect(returnUrl);
        }

        public IActionResult Roslyn()
        {
            return View(_atiehJobConfig.UseRoslynScripts);
        }

        [HttpPost]
        public IActionResult Roslyn(DataSourceRequest command)
        {
            var scripts = RoslynCompiler.ReferencedScripts != null ? RoslynCompiler.ReferencedScripts.ToList() : new List<ResultCompiler>();

            var gridModel = new DataSourceResult
            {
                Data = scripts.Select(x => new
                {
                    FileName = x.OriginalFile,
                    IsCompiled = x.IsCompiled,
                    Errors = string.Join(",", x.ErrorInfo)
                }),
                Total = scripts.Count
            };
            return Json(gridModel);
        }

        public IActionResult QueryEditor()
        {
            var model = new QueryEditor();
            return View(model);
        }

        [HttpPost]
        public IActionResult QueryEditor(string query)
        {
            //https://docs.mongodb.com/manual/reference/command/
            if (string.IsNullOrEmpty(query))
                return ErrorForKendoGridJson("Empty query");
            try
            {
                var result = _mongoDbContext.RunCommand<BsonDocument>(query);
                var ok = result.FirstOrDefault(x => x.Name == "ok").Value.ToBoolean();
                var gridModel = new DataSourceResult();
                if (result.Where(x => x.Name == "cursor").ToList().Any())
                {
                    var resultCollection = result["cursor"]["firstBatch"].AsBsonArray.ToList();
                    var response = Serialize(resultCollection);
                    var enumerable = response as Dictionary<string, object>[] ?? response.ToArray();
                    gridModel = new DataSourceResult
                    {
                        Data = enumerable,
                        Total = enumerable.Count()
                    };
                }
                else if (result.Where(x => x.Name == "n").ToList().Any())
                {
                    var n = new List<dynamic>();
                    var number = result["n"].ToInt64();
                    n.Add(new { Number = number });
                    gridModel = new DataSourceResult
                    {
                        Data = n
                    };
                }
                return Json(gridModel);
            }
            catch (Exception ex)
            {
                return ErrorForKendoGridJson(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult RunScript(string query)
        {
            if (string.IsNullOrEmpty(query))
                return Json(new { Result = false, Message = "Empty query!" });

            try
            {
                var bScript = new BsonJavaScript(query);
                var operation = new EvalOperation(_mongoDbContext.Database().DatabaseNamespace, bScript, null);
                var writeBinding = new WritableServerBinding(_mongoDbContext.Database().Client.Cluster, NoCoreSession.NewHandle());
                var result = operation.Execute(writeBinding, CancellationToken.None);
                var xx = result["_ns"];
                return Json(new { Result = true, Message = result.ToString() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        public IActionResult SeNames()
        {
            var model = new UrlRecordListModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult SeNames(DataSourceRequest command, UrlRecordListModel model)
        {
            var urlRecords = _urlRecordService.GetAllUrlRecords(model.SeName, command.Page - 1, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = urlRecords.Select(x =>
                {
                    //language
                    string languageName;
                    if (string.IsNullOrEmpty(x.LanguageId))
                    {
                        languageName = _localizationService.GetResource("Admin.System.SeNames.Language.Standard");
                    }
                    else
                    {
                        var language = _languageService.GetLanguageById(x.LanguageId);
                        languageName = language != null ? language.Name : "Unknown";
                    }

                    //details URL
                    var detailsUrl = "";
                    var entityName = x.EntityName != null ? x.EntityName.ToLowerInvariant() : "";
                    switch (entityName)
                    {
                        case "newsitem":
                            detailsUrl = Url.Action("Edit", "News", new { id = x.EntityId });
                            break;
                        case "topic":
                            detailsUrl = Url.Action("Edit", "Topic", new { id = x.EntityId });
                            break;
                        default:
                            break;
                    }

                    return new UrlRecordModel
                    {
                        Id = x.Id,
                        Name = x.Slug,
                        EntityId = x.EntityId,
                        EntityName = x.EntityName,
                        IsActive = x.IsActive,
                        Language = languageName,
                        DetailsUrl = detailsUrl
                    };
                }),
                Total = urlRecords.TotalCount
            };
            return Json(gridModel);
        }
        [HttpPost]
        public IActionResult DeleteSelectedSeNames(ICollection<string> selectedIds)
        {
            if (selectedIds == null)
            {
                return Json(new { Result = true });
            }

            var urlRecords = selectedIds.Select(id => _urlRecordService.GetUrlRecordById(id))
               .Where(urlRecord => urlRecord != null).ToList();
            foreach (var urlRecord in urlRecords)
                _urlRecordService.DeleteUrlRecord(urlRecord);

            return Json(new { Result = true });
        }


        #endregion
    }
}
