using System;
using System.IO;
using System.Linq;
using System.Text;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Services.Logging;
using AtiehJobCore.Services.Security;
using AtiehJobCore.Web.Framework.Filters;
using AtiehJobCore.Web.Framework.Infrastructure.Extensions;
using AtiehJobCore.Web.Framework.KendoUi;
using AtiehJobCore.Web.Framework.Models.Admin;
using AtiehJobCore.Web.Framework.Mvc;
using AtiehJobCore.Web.Framework.Security;
using AtiehJobCore.Web.Framework.Security.Authorization;
using AtiehJobCore.Web.Framework.Services.Admin;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtiehJobCore.Web.Areas.Admin.Controllers
{
    [PermissionAuthorize(PermissionSystemName.Languages)]
    [BreadCrumb(Title = "زبانها", UseDefaultRouteUrl = true, Order = 0)]
    public class LanguageController : BaseAdminController
    {
        private readonly ILanguageService _languageService;
        private readonly ILanguageViewModelService _languageViewModelService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;

        public LanguageController(ILanguageService languageService,
            ILanguageViewModelService languageViewModelService,
            ILocalizationService localizationService, ILogger logger)
        {
            _languageService = languageService;
            _languageViewModelService = languageViewModelService;
            _localizationService = localizationService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        [BreadCrumb(Title = "لیست زبانها", Order = 1)]
        public IActionResult List()
        {
            return View();
        }
        [HttpPost]
        public IActionResult List(DataSourceRequest command)
        {
            var languages = _languageService.GetAllLanguages(true);
            var gridModel = new DataSourceResult
            {
                Data = languages.Select(x => x.ToModel()),
                Total = languages.Count()
            };
            return Json(gridModel);
        }

        public IActionResult Create()
        {
            var model = new AdminLanguageModel();
            //flags
            _languageViewModelService.PrepareFlagsModel(model);
            //default values
            model.Published = true;
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public IActionResult Create(AdminLanguageModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var language = _languageViewModelService.InsertLanguageModel(model);
                //SuccessNotification(_localizationService.GetResource("Admin.Configuration.Languages.Added"));
                return continueEditing ? RedirectToAction("Edit", new { id = language.Id }) : RedirectToAction("List");
            }

            //If we got this far, something failed, redisplay form

            //flags
            _languageViewModelService.PrepareFlagsModel(model);

            return View(model);
        }

        public IActionResult Edit(string id)
        {
            var language = _languageService.GetLanguageById(id);
            if (language == null)
                //No language found with the specified id
                return RedirectToAction("List");

            var model = language.ToModel();
            //flags
            _languageViewModelService.PrepareFlagsModel(model);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public IActionResult Edit(AdminLanguageModel model, bool continueEditing)
        {
            var language = _languageService.GetLanguageById(model.Id);
            if (language == null)
                //No language found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                //ensure we have at least one published language
                var allLanguages = _languageService.GetAllLanguages();
                if (allLanguages.Count == 1 && allLanguages[0].Id == language.Id &&
                    !model.Published)
                {
                    //ErrorNotification("At least one published language is required.");
                    return RedirectToAction("Edit", new { id = language.Id });
                }

                language = _languageViewModelService.UpdateLanguageModel(language, model);
                //notification
                //SuccessNotification(_localizationService.GetResource("Admin.Configuration.Languages.Updated"));
                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();

                    return RedirectToAction("Edit", new { id = language.Id });
                }
                return RedirectToAction("List");
            }
            //If we got this far, something failed, redisplay form

            //flags
            _languageViewModelService.PrepareFlagsModel(model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var language = _languageService.GetLanguageById(id);
            if (language == null)
                //No language found with the specified id
                return RedirectToAction("List");

            //ensure we have at least one published language
            var allLanguages = _languageService.GetAllLanguages();
            if (allLanguages.Count == 1 && allLanguages[0].Id == language.Id)
            {
                //ErrorNotification("At least one published language is required.");
                return RedirectToAction("Edit", new { id = language.Id });
            }

            //delete
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = language.Id });
            }

            _languageService.DeleteLanguage(language);

            //notification
            //SuccessNotification(_localizationService.GetResource("Admin.Configuration.Languages.Deleted"));
            return RedirectToAction("List");
            //ErrorNotification(ModelState);
        }

        #region Resources

        [HttpPost]
        [AdminAntiForgery(true)]
        public IActionResult Resources(string languageId, DataSourceRequest command,
            AdminLanguageResourceFilterModel model)
        {
            var (languageResourceModels, totalCount) =
                _languageViewModelService.PrepareLanguageResourceModel(model, languageId, command.Page, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = languageResourceModels.ToList(),
                Total = totalCount
            };

            return Json(gridModel);
        }

        [HttpPost]
        public IActionResult ResourceUpdate(AdminLanguageResourceModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new DataSourceResult { Errors = ModelState.SerializeErrors() });
            }

            var (error, message) = _languageViewModelService.UpdateLanguageResourceModel(model);
            return error ? ErrorForKendoGridJson(message) : new NullJsonResult();
        }

        [HttpPost]
        public IActionResult ResourceAdd(AdminLanguageResourceModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new DataSourceResult { Errors = ModelState.SerializeErrors() });
            }
            var (error, message) = _languageViewModelService.InsertLanguageResourceModel(model);
            return error ? ErrorForKendoGridJson(message) : new NullJsonResult();
        }

        [HttpPost]
        public IActionResult ResourceDelete(string id)
        {
            var resource = _localizationService.GetLocaleStringResourceById(id);
            if (resource == null)
                throw new ArgumentException("No resource found with the specified id");
            if (!ModelState.IsValid)
            {
                return ErrorForKendoGridJson(ModelState);
            }

            _localizationService.DeleteLocaleStringResource(resource);
            return new NullJsonResult();
        }

        #endregion

        #region Export / Import

        public IActionResult ExportXml(string id)
        {
            var language = _languageService.GetLanguageById(id);
            if (language == null)
                //No language found with the specified id
                return RedirectToAction("List");

            try
            {
                var xml = _localizationService.ExportResourcesToXml(language);
                return File(Encoding.UTF8.GetBytes(xml), "application/xml", "language_pack.xml");
            }
            catch (Exception exc)
            {
                //ErrorNotification(exc);
                _logger.InsertLog(MongoLogLevel.Error, exc.Message, exc.StackTrace);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public IActionResult ImportXml(string id, IFormFile importXmlFile)
        {
            var language = _languageService.GetLanguageById(id);
            if (language == null)
                //No language found with the specified id
                return RedirectToAction("List");

            try
            {
                if (importXmlFile != null && importXmlFile.Length > 0)
                {
                    using (var sr = new StreamReader(importXmlFile.OpenReadStream(), Encoding.UTF8))
                    {
                        var content = sr.ReadToEnd();
                        _localizationService.ImportResourcesFromXml(language, content);
                    }
                }
                else
                {
                    //ErrorNotification(_localizationService.GetResource("Admin.Common.UploadFile"));
                    return RedirectToAction("Edit", new { id = language.Id });
                }

                //SuccessNotification(_localizationService.GetResource("Admin.Configuration.Languages.Imported"));
                return RedirectToAction("Edit", new { id = language.Id });
            }
            catch (Exception exc)
            {
                //ErrorNotification(exc);
                _logger.InsertLog(MongoLogLevel.Error, exc.Message, exc.StackTrace);
                return RedirectToAction("Edit", new { id = language.Id });
            }
        }

        #endregion
    }
}
