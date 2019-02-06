﻿using AtiehJobCore.Common.Configuration;
using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.Infrastructure.MongoDb;
using AtiehJobCore.Common.MongoDb;
using AtiehJobCore.Common.MongoDb.Domain.Localization;
using AtiehJobCore.Common.MongoDb.Domain.Security;
using AtiehJobCore.Common.Plugins;
using AtiehJobCore.Common.Utilities;
using AtiehJobCore.Services.MongoDb.Configuration;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AtiehJobCore.Services.MongoDb.Localization
{
    public static class LocalizationExtensions
    {
        /// <summary>
        /// Get localized property of an entity
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="keySelector">Key selector</param>
        /// <returns>Localized property</returns>
        public static string GetLocalized<T>(this T entity,
            Expression<Func<T, string>> keySelector)
            where T : ParentMongoEntity, ILocalizedEntity
        {
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            return GetLocalized(entity, keySelector, workContext.WorkingLanguage.Id);
        }

        /// <summary>
        /// Get localized property of an entity
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="returnDefaultValue">A value indicating whether to return default value (if localized is not found)</param>
        /// <param name="ensureTwoPublishedLanguages">A value indicating whether to ensure that we have at least two published languages; otherwise, load only default value</param>
        /// <returns>Localized property</returns>
        public static string GetLocalized<T>(this T entity,
            Expression<Func<T, string>> keySelector, string languageId,
            bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true)
            where T : ParentMongoEntity, ILocalizedEntity
        {
            return GetLocalized<T, string>(entity, keySelector, languageId, returnDefaultValue, ensureTwoPublishedLanguages);
        }

        /// <summary>
        /// Get localized property of an entity
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="returnDefaultValue">A value indicating whether to return default value (if localized is not found)</param>
        /// <param name="ensureTwoPublishedLanguages">A value indicating whether to ensure that we have at least two published languages; otherwise, load only default value</param>
        /// <returns>Localized property</returns>
        public static TPropType GetLocalized<T, TPropType>(this T entity,
            Expression<Func<T, TPropType>> keySelector, string languageId,
            bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true)
            where T : ParentMongoEntity, ILocalizedEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (!(keySelector.Body is MemberExpression member))
            {
                throw new ArgumentException($"Expression '{keySelector}' refers to a method, not a property.");
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException($"Expression '{keySelector}' refers to a field, not a property.");
            }

            var result = default(TPropType);
            var resultStr = string.Empty;

            //load localized value
            var localeKeyGroup = typeof(T).Name;
            var localeKey = propInfo.Name;

            if (!string.IsNullOrEmpty(languageId))
            {
                if (entity.Locales.Any())
                {
                    var en = entity.Locales.FirstOrDefault(x => x.LanguageId == languageId && x.LocaleKey == localeKey);
                    if (en != null)
                    {
                        resultStr = en.LocaleValue;
                        if (!string.IsNullOrEmpty(resultStr))
                            result = CommonHelper.To<TPropType>(resultStr);
                    }
                }
            }

            //set default value if required
            if (string.IsNullOrEmpty(resultStr) && returnDefaultValue)
            {
                result = (TPropType)(propInfo.GetValue(entity));
            }

            return result;
        }


        /// <summary>
        /// Get localized property of setting
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="settings">Settings</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="returnDefaultValue">A value indicating whether to return default value (if localized is not found)</param>
        /// <param name="ensureTwoPublishedLanguages">A value indicating whether to ensure that we have at least two published languages; otherwise, load only default value</param>
        /// <returns>Localized property</returns>
        public static string GetLocalizedSetting<T>(this T settings,
            Expression<Func<T, string>> keySelector, string languageId,
            bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true)
            where T : ISettings, new()
        {
            var settingService = EngineContext.Current.Resolve<ISettingService>();

            var key = settings.GetSettingKey(keySelector);

            var setting = settingService.GetSetting(key, loadSharedValueIfNotFound: true);

            return setting?.GetLocalized(x => x.Value, languageId, returnDefaultValue, ensureTwoPublishedLanguages);
        }

        /// <summary>
        /// Get localized value of enum
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="enumValue">Enum value</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="workContext">Work context</param>
        /// <returns>Localized value</returns>
        public static string GetLocalizedEnum<T>(this T enumValue, ILocalizationService localizationService, IWorkContext workContext)
            where T : struct
        {
            if (workContext == null)
                throw new ArgumentNullException(nameof(workContext));

            return GetLocalizedEnum(enumValue, localizationService, workContext.WorkingLanguage.Id);
        }
        /// <summary>
        /// Get localized value of enum
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="enumValue">Enum value</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Localized value</returns>
        public static string GetLocalizedEnum<T>(this T enumValue, ILocalizationService localizationService, string languageId)
            where T : struct
        {
            if (localizationService == null)
                throw new ArgumentNullException(nameof(localizationService));

            if (!typeof(T).GetTypeInfo().IsEnum) throw new ArgumentException("T must be an enumerated type");

            //localized value
            var resourceName = $"Enums.{typeof(T)}.{enumValue.ToString()}";
            var result = localizationService.GetResource(resourceName, languageId, false, "", true);

            //set default value if required
            if (string.IsNullOrEmpty(result))
                result = CommonHelper.ConvertEnum(enumValue.ToString());

            return result;
        }


        /// <summary>
        /// Get localized value of permission
        /// We don't have UI to manage permission localizable name. That's why we're using this extension method
        /// </summary>
        /// <param name="permissionRecord">Permission record</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="workContext">Work context</param>
        /// <returns>Localized value</returns>
        public static string GetLocalizedPermissionName(this PermissionRecord permissionRecord,
            ILocalizationService localizationService, IWorkContext workContext)
        {
            if (workContext == null)
                throw new ArgumentNullException(nameof(workContext));

            return GetLocalizedPermissionName(permissionRecord, localizationService, workContext.WorkingLanguage.Id);
        }
        /// <summary>
        /// Get localized value of enum
        /// We don't have UI to manage permission localizable name. That's why we're using this extension method
        /// </summary>
        /// <param name="permissionRecord">Permission record</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Localized value</returns>
        public static string GetLocalizedPermissionName(this PermissionRecord permissionRecord,
            ILocalizationService localizationService, string languageId)
        {
            if (permissionRecord == null)
                throw new ArgumentNullException(nameof(permissionRecord));

            if (localizationService == null)
                throw new ArgumentNullException(nameof(localizationService));

            //localized value
            var resourceName = $"Permission.{permissionRecord.SystemName}";
            var result = localizationService.GetResource(resourceName, languageId, false, "", true);

            //set default value if required
            if (string.IsNullOrEmpty(result))
                result = permissionRecord.Name;

            return result;
        }
        /// <summary>
        /// Save localized name of a permission
        /// </summary>
        /// <param name="permissionRecord">Permission record</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="languageService">Language service</param>
        public static void SaveLocalizedPermissionName(this PermissionRecord permissionRecord,
            ILocalizationService localizationService, ILanguageService languageService)
        {
            if (permissionRecord == null)
                throw new ArgumentNullException(nameof(permissionRecord));
            if (localizationService == null)
                throw new ArgumentNullException(nameof(localizationService));
            if (languageService == null)
                throw new ArgumentNullException(nameof(languageService));

            var resourceName = $"Permission.{permissionRecord.SystemName}";
            var resourceValue = permissionRecord.Name;

            foreach (var lang in languageService.GetAllLanguages(true))
            {
                var lsr = localizationService.GetLocaleStringResourceByName(resourceName, lang.Id, false);
                if (lsr == null)
                {
                    lsr = new LocaleStringResource
                    {
                        LanguageId = lang.Id,
                        ResourceName = resourceName,
                        ResourceValue = resourceValue
                    };
                    localizationService.InsertLocaleStringResource(lsr);
                }
                else
                {
                    lsr.ResourceValue = resourceValue;
                    localizationService.UpdateLocaleStringResource(lsr);
                }
            }
        }
        /// <summary>
        /// Delete a localized name of a permission
        /// </summary>
        /// <param name="permissionRecord">Permission record</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="languageService">Language service</param>
        public static void DeleteLocalizedPermissionName(this PermissionRecord permissionRecord,
            ILocalizationService localizationService, ILanguageService languageService)
        {
            if (permissionRecord == null)
                throw new ArgumentNullException(nameof(permissionRecord));
            if (localizationService == null)
                throw new ArgumentNullException(nameof(localizationService));
            if (languageService == null)
                throw new ArgumentNullException(nameof(languageService));

            var resourceName = $"Permission.{permissionRecord.SystemName}";
            foreach (var lang in languageService.GetAllLanguages(true))
            {
                var lsr = localizationService.GetLocaleStringResourceByName(resourceName, lang.Id, false);
                if (lsr != null)
                    localizationService.DeleteLocaleStringResource(lsr);
            }
        }



        /// <summary>
        /// Delete a locale resource
        /// </summary>
        /// <param name="plugin">Plugin</param>
        /// <param name="resourceName">Resource name</param>
        public static void DeletePluginLocaleResource(this BasePlugin plugin,
            string resourceName)
        {
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var languageService = EngineContext.Current.Resolve<ILanguageService>();
            DeletePluginLocaleResource(plugin, localizationService,
                languageService, resourceName.ToLowerInvariant());
        }
        /// <summary>
        /// Delete a locale resource
        /// </summary>
        /// <param name="plugin">Plugin</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="languageService">Language service</param>
        /// <param name="resourceName">Resource name</param>
        public static void DeletePluginLocaleResource(this BasePlugin plugin,
            ILocalizationService localizationService, ILanguageService languageService,
            string resourceName)
        {
            //actually plugin instance is not required
            if (plugin == null)
                throw new ArgumentNullException(nameof(plugin));
            if (localizationService == null)
                throw new ArgumentNullException(nameof(localizationService));
            if (languageService == null)
                throw new ArgumentNullException(nameof(languageService));
            if (string.IsNullOrEmpty(resourceName))
            {
                resourceName = resourceName?.ToLowerInvariant();
            }

            foreach (var lang in languageService.GetAllLanguages(true))
            {
                var lsr = localizationService.GetLocaleStringResourceByName(resourceName, lang.Id, false);
                if (lsr != null)
                    localizationService.DeleteLocaleStringResource(lsr);
            }
        }
        /// <summary>
        /// Add a locale resource (if new) or update an existing one
        /// </summary>
        /// <param name="plugin">Plugin</param>
        /// <param name="resourceName">Resource name</param>
        /// <param name="resourceValue">Resource value</param>
        /// <param name="languageCulture">Language culture code. If null or empty, then a resource will be added for all languages</param>
        public static void AddOrUpdatePluginLocaleResource(this BasePlugin plugin,
            string resourceName, string resourceValue, string languageCulture = null)
        {
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var languageService = EngineContext.Current.Resolve<ILanguageService>();
            AddOrUpdatePluginLocaleResource(plugin, localizationService,
                languageService, resourceName, resourceValue, languageCulture);
        }
        /// <summary>
        /// Add a locale resource (if new) or update an existing one
        /// </summary>
        /// <param name="plugin">Plugin</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="languageService">Language service</param>
        /// <param name="resourceName">Resource name</param>
        /// <param name="resourceValue">Resource value</param>
        /// <param name="languageCulture">Language culture code. If null or empty, then a resource will be added for all languages</param>
        public static void AddOrUpdatePluginLocaleResource(this BasePlugin plugin,
            ILocalizationService localizationService, ILanguageService languageService,
            string resourceName, string resourceValue, string languageCulture = null)
        {
            //actually plugin instance is not required
            if (plugin == null)
                throw new ArgumentNullException(nameof(plugin));
            if (localizationService == null)
                throw new ArgumentNullException(nameof(localizationService));
            if (languageService == null)
                throw new ArgumentNullException(nameof(languageService));
            if (string.IsNullOrEmpty(resourceName))
                resourceName = resourceName?.ToLowerInvariant();
            foreach (var lang in languageService.GetAllLanguages(true))
            {
                if (!string.IsNullOrEmpty(languageCulture) && !languageCulture.Equals(lang.LanguageCulture))
                    continue;

                var lsr = localizationService.GetLocaleStringResourceByName(resourceName, lang.Id, false);
                if (lsr == null)
                {
                    lsr = new LocaleStringResource
                    {
                        LanguageId = lang.Id,
                        ResourceName = resourceName,
                        ResourceValue = resourceValue
                    };
                    localizationService.InsertLocaleStringResource(lsr);
                }
                else
                {
                    lsr.ResourceValue = resourceValue;
                    localizationService.UpdateLocaleStringResource(lsr);
                }
            }
        }



        /// <summary>
        /// Get localized friendly name of a plugin
        /// </summary>
        /// <typeparam name="T">Plugin</typeparam>
        /// <param name="plugin">Plugin</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="returnDefaultValue">A value indicating whether to return default value (if localized is not found)</param>
        /// <returns>Localized value</returns>
        public static string GetLocalizedFriendlyName<T>(this T plugin, ILocalizationService localizationService,
            string languageId, bool returnDefaultValue = true)
            where T : IPlugin
        {
            if (localizationService == null)
                throw new ArgumentNullException(nameof(localizationService));

            if (plugin == null)
                throw new ArgumentNullException(nameof(plugin));

            if (plugin.PluginDescriptor == null)
                throw new ArgumentException("Plugin descriptor cannot be loaded");

            var systemName = plugin.PluginDescriptor.SystemName;
            //localized value
            var resourceName = $"Plugins.FriendlyName.{systemName}";
            var result = localizationService.GetResource(resourceName, languageId, false, "", true);

            //set default value if required
            if (string.IsNullOrEmpty(result) && returnDefaultValue)
                result = plugin.PluginDescriptor.FriendlyName;

            return result;
        }
        /// <summary>
        /// Save localized friendly name of a plugin
        /// </summary>
        /// <typeparam name="T">Plugin</typeparam>
        /// <param name="plugin">Plugin</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="localizedFriendlyName">Localized friendly name</param>
        public static void SaveLocalizedFriendlyName<T>(this T plugin,
            ILocalizationService localizationService, string languageId,
            string localizedFriendlyName)
            where T : IPlugin
        {
            if (localizationService == null)
                throw new ArgumentNullException(nameof(localizationService));

            if (string.IsNullOrEmpty(languageId))
                throw new ArgumentOutOfRangeException(nameof(languageId), "Language ID should not be 0");

            if (plugin == null)
                throw new ArgumentNullException(nameof(plugin));

            if (plugin.PluginDescriptor == null)
                throw new ArgumentException("Plugin descriptor cannot be loaded");

            var systemName = plugin.PluginDescriptor.SystemName;

            //localized value
            var resourceName = $"Plugins.FriendlyName.{systemName}";
            if (string.IsNullOrEmpty(resourceName))
                resourceName = resourceName.ToLowerInvariant();

            var resource = localizationService.GetLocaleStringResourceByName(resourceName, languageId, false);

            if (resource != null)
            {
                if (string.IsNullOrWhiteSpace(localizedFriendlyName))
                {
                    //delete
                    localizationService.DeleteLocaleStringResource(resource);
                }
                else
                {
                    //update
                    resource.ResourceValue = localizedFriendlyName;
                    localizationService.UpdateLocaleStringResource(resource);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(localizedFriendlyName))
                {
                    return;
                }

                //insert
                resource = new LocaleStringResource
                {
                    LanguageId = languageId,
                    ResourceName = resourceName,
                    ResourceValue = localizedFriendlyName,
                };
                localizationService.InsertLocaleStringResource(resource);
            }
        }
    }
}
