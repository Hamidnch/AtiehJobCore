using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Localization;
using AtiehJobCore.Core.Extensions;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Events;
using AtiehJobCore.Services.Logging;
using MongoDB.Driver;

namespace AtiehJobCore.Services.Localization
{
    /// <inheritdoc />
    /// <summary>
    /// Provides information about localization
    /// </summary>
    public partial class LocalizationService : ILocalizationService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : language ID
        /// </remarks>
        private const string LocalStringResourcesAllKey = "AtiehJob.lsr.all-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : language ID
        /// {1} : resource key
        /// </remarks>
        private const string LocalStringResourcesByResourceNameKey = "AtiehJob.lsr.{0}-{1}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string LocalStringResourcesPatternKey = "AtiehJob.lsr.";

        #endregion

        #region Fields

        private readonly IRepository<LocaleStringResource> _lsrRepository;
        private readonly ILogger _logger;
        private readonly IWorkContext _workContext;
        private readonly ILanguageService _languageService;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;
        private readonly LocalizationSettings _localizationSettings;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="logger">Logger</param>
        /// <param name="lsrRepository">Locale string resource repository</param>
        /// <param name="languageService"></param>
        /// <param name="localizationSettings">Localization settings</param>
        /// <param name="eventPublisher"></param>
        public LocalizationService(ICacheManager cacheManager,
            ILogger logger, IRepository<LocaleStringResource> lsrRepository,
            ILanguageService languageService,
            LocalizationSettings localizationSettings,
            IEventPublisher eventPublisher, IWorkContext workContext)
        {
            _cacheManager = cacheManager;
            _logger = logger;
            _lsrRepository = lsrRepository;
            _languageService = languageService;
            _localizationSettings = localizationSettings;
            _eventPublisher = eventPublisher;
            _workContext = workContext;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Deletes a locale string resource
        /// </summary>
        /// <param name="localeStringResource">Locale string resource</param>
        public virtual void DeleteLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException(paramName: nameof(localeStringResource));

            _lsrRepository.Delete(entity: localeStringResource);

            //cache
            _cacheManager.RemoveByPattern(pattern: LocalStringResourcesPatternKey);

            //event notification
            _eventPublisher.EntityDeleted(entity: localeStringResource);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a locale string resource
        /// </summary>
        /// <param name="localeStringResourceId">Locale string resource identifier</param>
        /// <returns>Locale string resource</returns>
        public virtual LocaleStringResource GetLocaleStringResourceById(string localeStringResourceId)
        {
            return string.IsNullOrEmpty(value: localeStringResourceId) ? null : _lsrRepository.GetById(id: localeStringResourceId);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a locale string resource
        /// </summary>
        /// <param name="resourceName">A string representing a resource name</param>
        /// <returns>Locale string resource</returns>
        public virtual LocaleStringResource GetLocaleStringResourceByName(string resourceName)
        {
            var languageId = _languageService.GetAllLanguages().FirstOrDefault()?.Id;
            return GetLocaleStringResourceByName(resourceName: resourceName, languageId: languageId);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a locale string resource
        /// </summary>
        /// <param name="resourceName">A string representing a resource name</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="logIfNotFound">A value indicating whether to log error if locale string resource is not found</param>
        /// <returns>Locale string resource</returns>
        public virtual LocaleStringResource GetLocaleStringResourceByName(string resourceName,
            string languageId, bool logIfNotFound = true)
        {
            var query = from lsr in _lsrRepository.Table
                        orderby lsr.ResourceName
                        where lsr.LanguageId == languageId && lsr.ResourceName == resourceName
                        select lsr;
            var localeStringResource = query.FirstOrDefault();

            if (localeStringResource == null && logIfNotFound)
                _logger.Warning(message: string.Format(format: "Resource string ({0}) not found. Language ID = {1}", arg0: resourceName, arg1: languageId));
            return localeStringResource;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets all locale string resources by language identifier
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Locale string resources</returns>
        public virtual IList<LocaleStringResource> GetAllResources(string languageId)
        {
            var filter = Builders<LocaleStringResource>.Filter.Eq(field: x => x.LanguageId, value: languageId);
            return _lsrRepository.Collection.Find(filter: filter).ToList();
        }

        /// <inheritdoc />
        /// <summary>
        /// Inserts a locale string resource
        /// </summary>
        /// <param name="localeStringResource">Locale string resource</param>
        public virtual void InsertLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException(paramName: nameof(localeStringResource));

            localeStringResource.ResourceName = localeStringResource.ResourceName.ToLowerInvariant();
            _lsrRepository.Insert(entity: localeStringResource);

            //cache
            _cacheManager.RemoveByPattern(pattern: LocalStringResourcesPatternKey);

            //event notification
            _eventPublisher.EntityInserted(entity: localeStringResource);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the locale string resource
        /// </summary>
        /// <param name="localeStringResource">Locale string resource</param>
        public virtual void UpdateLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException(paramName: nameof(localeStringResource));

            localeStringResource.ResourceName = localeStringResource.ResourceName.ToLowerInvariant();
            _lsrRepository.Update(entity: localeStringResource);

            //cache
            _cacheManager.RemoveByPattern(pattern: LocalStringResourcesPatternKey);

            //event notification
            _eventPublisher.EntityUpdated(entity: localeStringResource);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a resource string based on the specified ResourceKey property.
        /// </summary>
        /// <param name="resourceKey">A string representing a ResourceKey.</param>
        /// <returns>A string representing the requested resource string.</returns>
        public virtual string GetResource(string resourceKey)
        {
            return _workContext.WorkingLanguage != null
                ? GetResource(resourceKey, _workContext.WorkingLanguage.Id) : "";
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a resource string based on the specified ResourceKey property.
        /// </summary>
        /// <param name="resourceKey">A string representing a ResourceKey.</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="logIfNotFound">A value indicating whether to log error if locale string resource is not found</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="returnEmptyIfNotFound">A value indicating whether an empty string will be returned if a resource is not found and default value is set to empty string</param>
        /// <returns>A string representing the requested resource string.</returns>
        public virtual string GetResource(string resourceKey, string languageId,
            bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false)
        {
            var result = string.Empty;
            if (resourceKey == null)
                resourceKey = string.Empty;
            resourceKey = resourceKey.Trim().ToLowerInvariant();
            if (_localizationSettings.LoadAllLocaleRecordsOnStartup)
            {
                //load all records (cached)
                var key = string.Format(format: LocalStringResourcesAllKey, arg0: languageId);
                var resources = _cacheManager.Get(key: key, acquire: () =>
                {
                    var dictionary = new Dictionary<string, LocaleStringResource>();
                    var locales = GetAllResources(languageId: languageId);
                    foreach (var locale in locales)
                    {
                        var resourceName = locale.ResourceName.ToLowerInvariant();
                        if (!dictionary.ContainsKey(key: resourceName))
                            dictionary.Add(key: resourceName.ToLowerInvariant(), value: locale);
                        else
                        {
                            _lsrRepository.Delete(entity: locale);
                        }
                    }
                    return dictionary;
                });
                if (resources.ContainsKey(key: resourceKey.ToLowerInvariant()))
                    result = resources[key: resourceKey.ToLowerInvariant()].ResourceValue;
            }
            else
            {
                //gradual loading
                var key = string.Format(format: LocalStringResourcesByResourceNameKey, arg0: languageId, arg1: resourceKey);
                var lsr = _cacheManager.Get(key: key, acquire: () =>
                {
                    var builder = Builders<LocaleStringResource>.Filter;
                    var filter = builder.Eq(field: x => x.LanguageId, value: languageId);
                    filter = filter & builder.Eq(field: x => x.ResourceName, value: resourceKey.ToLowerInvariant());
                    return _lsrRepository.Collection.Find(filter: filter).FirstOrDefault()?.ResourceValue;
                });

                if (lsr != null)
                    result = lsr;
            }

            if (!string.IsNullOrEmpty(value: result))
            {
                return result;
            }

            if (logIfNotFound)
                _logger.Warning(message: string.Format(format: "Resource string ({0}) is not found. Language ID = {1}", arg0: resourceKey, arg1: languageId));

            if (!string.IsNullOrEmpty(value: defaultValue))
            {
                result = defaultValue;
            }
            else
            {
                if (!returnEmptyIfNotFound)
                    result = resourceKey;
            }
            return result;
        }

        /// <inheritdoc />
        /// <summary>
        /// Export language resources to xml
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>Result in XML format</returns>
        public virtual string ExportResourcesToXml(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(paramName: nameof(language));
            var sb = new StringBuilder();

            var xwSettings = new XmlWriterSettings
            {
                ConformanceLevel = ConformanceLevel.Auto
            };

            using (var stringWriter = new StringWriter(sb: sb))
            using (var xmlWriter = XmlWriter.Create(output: stringWriter, settings: xwSettings))
            {

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement(localName: "Language");
                xmlWriter.WriteAttributeString(localName: "Name", value: language.Name);

                var resources = GetAllResources(languageId: language.Id);
                foreach (var resource in resources)
                {
                    xmlWriter.WriteStartElement(localName: "LocaleResource");
                    xmlWriter.WriteAttributeString(localName: "Name", value: resource.ResourceName);
                    xmlWriter.WriteElementString(localName: "Value", ns: null, value: resource.ResourceValue);
                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                return stringWriter.ToString();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Import language resources from XML file
        /// </summary>
        /// <param name="language">Language</param>
        /// <param name="xml">XML</param>
        public virtual void ImportResourcesFromXml(Language language, string xml)
        {
            if (language == null)
                throw new ArgumentNullException(paramName: nameof(language));

            if (string.IsNullOrEmpty(value: xml))
                return;

            //stored procedures aren't supported
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml: xml);

            var nodes = xmlDoc.SelectNodes(xpath: @"//Language/LocaleResource");
            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes == null)
                    {
                        continue;
                    }

                    var name = node.Attributes[name: "Name"].InnerText.Trim();
                    var value = "";
                    var valueNode = node.SelectSingleNode(xpath: "Value");
                    if (valueNode != null)
                        value = valueNode.InnerText;

                    if (string.IsNullOrEmpty(value: name))
                        continue;

                    //do not use "Insert"/"Update" methods because they clear cache
                    //let's bulk insert

                    var resource = (from l in _lsrRepository.Table
                                    where l.ResourceName.ToLowerInvariant() == name.ToLowerInvariant()
                                          && l.LanguageId == language.Id
                                    select l).FirstOrDefault();

                    if (resource != null)
                    {
                        resource.ResourceName = resource.ResourceName.ToLowerInvariant();
                        resource.ResourceValue = value;
                        _lsrRepository.Update(entity: resource);
                    }
                    else
                    {
                        var lsr = (
                            new LocaleStringResource
                            {
                                LanguageId = language.Id,
                                ResourceName = name.ToLowerInvariant(),
                                ResourceValue = value
                            });
                        _lsrRepository.Insert(entity: lsr);
                    }
                }
            }

            //clear cache
            _cacheManager.RemoveByPattern(pattern: LocalStringResourcesPatternKey);
        }

        /// <inheritdoc />
        /// <summary>
        /// Import language resources from XML file
        /// </summary>
        /// <param name="language">Language</param>
        /// <param name="xml">XML</param>
        public virtual void ImportResourcesFromXmlInstall(Language language, string xml)
        {
            if (language == null)
                throw new ArgumentNullException(paramName: nameof(language));

            if (string.IsNullOrEmpty(value: xml))
                return;
            //stored procedures aren't supported
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml: xml);

            var nodes = xmlDoc.SelectNodes(xpath: @"//Language/LocaleResource");
            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes == null)
                    {
                        continue;
                    }

                    var name = node.Attributes[name: "Name"].InnerText.Trim();
                    var value = "";
                    var valueNode = node.SelectSingleNode(xpath: "Value");
                    if (valueNode != null)
                        value = valueNode.InnerText;

                    if (string.IsNullOrEmpty(value: name))
                        continue;

                    var lsr = (
                        new LocaleStringResource
                        {
                            LanguageId = language.Id,
                            ResourceName = name.ToLowerInvariant(),
                            ResourceValue = value
                        });
                    _lsrRepository.Insert(entity: lsr);
                }
            }

            //clear cache
            _cacheManager.RemoveByPattern(pattern: LocalStringResourcesPatternKey);
        }

        #endregion
    }
}
