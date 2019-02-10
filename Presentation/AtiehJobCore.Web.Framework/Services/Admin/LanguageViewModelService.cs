using AtiehJobCore.Core.Domain.Localization;
using AtiehJobCore.Core.Utilities;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Web.Framework.Infrastructure.Extensions;
using AtiehJobCore.Web.Framework.Models.Admin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AtiehJobCore.Web.Framework.Services.Admin
{
    public partial class LanguageViewModelService : ILanguageViewModelService
    {
        #region Fields

        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Constructors

        public LanguageViewModelService(ILanguageService languageService,
            ILocalizationService localizationService)
        {
            _localizationService = localizationService;
            _languageService = languageService;
        }

        #endregion

        public virtual void PrepareFlagsModel(AdminLanguageModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            model.FlagFileNames = Directory
                .EnumerateFiles(CommonHelper.MapPath("~/wwwroot/img/flags/"), "*.png", SearchOption.TopDirectoryOnly)
                .Select(Path.GetFileName)
                .ToList();
        }

        public virtual Language InsertLanguageModel(AdminLanguageModel model)
        {
            var language = model.ToEntity();
            _languageService.InsertLanguage(language);
            return language;
        }
        public virtual Language UpdateLanguageModel(Language language, AdminLanguageModel model)
        {
            //update
            language = model.ToEntity(language);
            _languageService.UpdateLanguage(language);
            return language;
        }
        public virtual (bool error, string message) InsertLanguageResourceModel(AdminLanguageResourceModel model)
        {
            if (model.Name != null)
                model.Name = model.Name.Trim();
            if (model.Value != null)
                model.Value = model.Value.Trim();

            var res = _localizationService.GetLocaleStringResourceByName(model.Name, model.LanguageId, false);
            if (res == null)
            {
                var resource = new LocaleStringResource
                {
                    LanguageId = model.LanguageId,
                    ResourceName = model.Name,
                    ResourceValue = model.Value
                };
                _localizationService.InsertLocaleStringResource(resource);
            }
            else
            {
                return (error: true, message: string.Format(
                    _localizationService.GetResource("Admin.Configuration.Languages.Resources.NameAlreadyExists"), model.Name));
            }
            return (false, string.Empty);
        }
        public virtual (bool error, string message) UpdateLanguageResourceModel(AdminLanguageResourceModel model)
        {
            if (model.Name != null)
                model.Name = model.Name.Trim();
            if (model.Value != null)
                model.Value = model.Value.Trim();
            var resource = _localizationService.GetLocaleStringResourceById(model.Id);
            // if the resourceName changed, ensure it isn't being used by another resource
            if (!resource.ResourceName.Equals(model.Name, StringComparison.OrdinalIgnoreCase))
            {
                var res = _localizationService.GetLocaleStringResourceByName(model.Name, model.LanguageId, false);
                if (res != null && res.Id != resource.Id)
                {
                    return (error: true, message: string.Format(
                        _localizationService.GetResource("Admin.Configuration.Languages.Resources.NameAlreadyExists"), res.ResourceName));
                }
            }
            resource.ResourceName = model.Name;
            resource.ResourceValue = model.Value;
            _localizationService.UpdateLocaleStringResource(resource);
            return (false, string.Empty);
        }
        public virtual (IEnumerable<AdminLanguageResourceModel> languageResourceModels, int totalCount)
            PrepareLanguageResourceModel(AdminLanguageResourceFilterModel model, string languageId, int pageIndex, int pageSize)
        {
            var language = _languageService.GetLanguageById(languageId);

            var resources = _localizationService
                .GetAllResources(languageId)
                .OrderBy(x => x.ResourceName)
                .Select(x => new AdminLanguageResourceModel
                {
                    LanguageId = languageId,
                    Id = x.Id,
                    Name = x.ResourceName,
                    Value = x.ResourceValue,
                });

            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.ResourceName))
                    resources = resources.Where(x => x.Name.ToLowerInvariant().Contains(model.ResourceName.ToLowerInvariant()));
                if (!string.IsNullOrEmpty(model.ResourceValue))
                    resources = resources.Where(x => x.Value.ToLowerInvariant().Contains(model.ResourceValue.ToLowerInvariant()));
            }
            resources = resources.AsQueryable();
            var adminLanguageResourceModels = resources.ToArray();
            return (adminLanguageResourceModels.Skip((pageIndex - 1) * pageSize).Take(pageSize), adminLanguageResourceModels.Count());
        }
    }
}
