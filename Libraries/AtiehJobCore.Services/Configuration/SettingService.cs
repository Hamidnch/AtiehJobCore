﻿using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Configuration;
using AtiehJobCore.Core.Domain.Common;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.Extensions;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Core.Utilities;
using AtiehJobCore.Services.Events;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AtiehJobCore.Services.Configuration
{
    /// <inheritdoc />
    /// <summary>
    /// Setting manager
    /// </summary>
    public partial class SettingService : ISettingService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        private const string SettingsAllKey = "AtiehJob.setting.all";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string SettingsPatternKey = "AtiehJob.setting.";

        #endregion

        #region Fields

        private readonly IRepository<Setting> _settingRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;

        private IDictionary<string, IList<SettingForCaching>> _allSettings = null;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="settingRepository">Setting repository</param>
        public SettingService(ICacheManager cacheManager, IEventPublisher eventPublisher,
            IRepository<Setting> settingRepository)
        {
            this._cacheManager = cacheManager;
            this._eventPublisher = eventPublisher;
            this._settingRepository = settingRepository;
        }

        #endregion

        #region Nested classes

        //[Serializable]
        public class SettingForCaching
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Gets all settings
        /// </summary>
        /// <returns>Settings</returns>
        protected virtual IDictionary<string, IList<SettingForCaching>> GetAllSettingsCached()
        {
            if (_allSettings != null)
                return _allSettings;

            //cache
            var key = string.Format(SettingsAllKey);
            _allSettings = _cacheManager.Get(key, () =>
            {
                //we use no tracking here for performance optimization
                //anyway records are loaded only for read-only operations
                var query = from s in _settingRepository.Table
                            orderby s.Name
                            select s;

                var settings = query.ToList();
                var dictionary = new Dictionary<string, IList<SettingForCaching>>();
                foreach (var s in settings)
                {
                    var resourceName = s.Name.ToLowerInvariant();
                    var settingForCaching = new SettingForCaching
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Value = s.Value
                    };
                    if (!dictionary.ContainsKey(resourceName))
                    {
                        //first setting
                        dictionary.Add(resourceName, new List<SettingForCaching>
                        {
                            settingForCaching
                        });
                    }
                    else
                    {
                        //already added
                        //most probably it's the setting with the same name
                        dictionary[resourceName].Add(settingForCaching);
                    }
                }
                return dictionary;
            });
            return _allSettings;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        public virtual void InsertSetting(Setting setting, bool clearCache = true)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            _settingRepository.Insert(setting);

            //cache
            if (clearCache)
                _cacheManager.RemoveByPattern(SettingsPatternKey);

        }

        /// <summary>
        /// Updates a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        public virtual void UpdateSetting(Setting setting, bool clearCache = true)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            _settingRepository.Update(setting);

            //cache
            if (clearCache)
                _cacheManager.RemoveByPattern(SettingsPatternKey);

        }

        /// <inheritdoc />
        /// <summary>
        /// Deletes a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        public virtual void DeleteSetting(Setting setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            _settingRepository.Delete(setting);

            //cache
            _cacheManager.RemoveByPattern(SettingsPatternKey);

        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a setting by identifier
        /// </summary>
        /// <param name="settingId">Setting identifier</param>
        /// <returns>Setting</returns>
        public virtual Setting GetSettingById(string settingId)
        {
            return _settingRepository.GetById(settingId);
        }

        /// <summary>
        /// Get setting by key
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="loadSharedValueIfNotFound">A value indicating whether a shared value should be loaded if a value specific for a certain is not found</param>
        /// <returns>Setting</returns>
        public virtual Setting GetSetting(string key, bool loadSharedValueIfNotFound = false)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            var settings = GetAllSettingsCached();
            key = key.Trim().ToLowerInvariant();
            if (!settings.ContainsKey(key))
            {
                return null;
            }

            var settingsByKey = settings[key];
            var setting = settingsByKey.FirstOrDefault();

            //load shared value?
            if (setting == null && loadSharedValueIfNotFound)
                setting = settingsByKey.FirstOrDefault();

            if (setting != null)
                return GetSettingById(setting.Id);

            return null;
        }

        /// <summary>
        /// Get setting value by key
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="loadSharedValueIfNotFound">A value indicating whether a shared value should be loaded if a value specific for a certain is not found</param>
        /// <returns>Setting value</returns>
        public virtual T GetSettingByKey<T>(string key, T defaultValue = default(T),
           bool loadSharedValueIfNotFound = false)
        {
            if (string.IsNullOrEmpty(key))
                return defaultValue;

            var settings = GetAllSettingsCached();
            key = key.Trim().ToLowerInvariant();
            if (!settings.ContainsKey(key))
            {
                return defaultValue;
            }

            var settingsByKey = settings[key];
            var setting = settingsByKey.FirstOrDefault();

            //load shared value?
            if (setting == null && loadSharedValueIfNotFound)
                setting = settingsByKey.FirstOrDefault();

            return setting != null ? CommonHelper.To<T>(setting.Value) : defaultValue;
        }

        /// <inheritdoc />
        /// <summary>
        /// Set setting value
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        public virtual void SetSetting<T>(string key, T value, bool clearCache = true)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            key = key.Trim().ToLowerInvariant();
            var valueStr = TypeDescriptor.GetConverter(typeof(T)).ConvertToInvariantString(value);

            var allSettings = GetAllSettingsCached();
            var settingForCaching = allSettings.ContainsKey(key) ?
                allSettings[key].FirstOrDefault() : null;
            if (settingForCaching != null)
            {
                //update
                var setting = GetSettingById(settingForCaching.Id);
                setting.Value = valueStr;
                UpdateSetting(setting, clearCache);
            }
            else
            {
                //insert
                var setting = new Setting
                {
                    Name = key,
                    Value = valueStr
                };
                InsertSetting(setting, clearCache);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets all settings
        /// </summary>
        /// <returns>Settings</returns>
        public virtual IList<Setting> GetAllSettings()
        {
            return _settingRepository.Collection.Find(new BsonDocument()).SortBy(x => x.Name).ToList();
        }

        /// <summary>
        /// Determines whether a setting exists
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="settings">Entity</param>
        /// <param name="keySelector">Key selector</param>
        /// <returns>true -setting exists; false - does not exist</returns>
        public virtual bool SettingExists<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector)
            where T : ISettings, new()
        {
            var key = settings.GetSettingKey(keySelector);

            var setting = GetSettingByKey<string>(key);
            return setting != null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Load settings
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public virtual T LoadSetting<T>() where T : ISettings, new()
        {
            var cacheKey = $"{SettingsPatternKey}{typeof(T).Name}";
            return _cacheManager.Get<T>(cacheKey, () => (T)LoadSetting(typeof(T)));
        }

        /// <inheritdoc />
        /// <summary>
        /// Load settings
        /// </summary>
        /// <param name="type">Type</param>
        public virtual ISettings LoadSetting(Type type)
        {
            var settings = Activator.CreateInstance(type);

            foreach (var prop in type.GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var key = type.Name + "." + prop.Name;

                var setting = GetSettingByKey<string>(key, loadSharedValueIfNotFound: true);
                if (string.IsNullOrEmpty(setting))
                    continue;

                var converter = TypeDescriptor.GetConverter(prop.PropertyType);

                if (!converter.CanConvertFrom(typeof(string)))
                    continue;
                try
                {
                    var value = converter.ConvertFromInvariantString(setting);
                    //set property
                    prop.SetValue(settings, value, null);
                }
                catch (Exception ex)
                {
                    var msg = $"Could not convert setting {key} to type {prop.PropertyType.FullName}";
                    Core.Infrastructure.EngineContext.Current.Resolve<Logging.ILogger>().InsertLog(MongoLogLevel.Error, msg, ex.Message);
                }
            }

            return settings as ISettings;
        }

        /// <inheritdoc />
        /// <summary>
        /// Save settings object
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="settings">Setting instance</param>
        public virtual void SaveSetting<T>(T settings) where T : ISettings, new()
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                var key = typeof(T).Name + "." + prop.Name;
                //Duck typing is not supported in C#. That's why we're using dynamic type
                dynamic value = prop.GetValue(settings, null);
                SetSetting(key, value != null ? value : "", false);
            }

            //and now clear cache
            ClearCache();
        }

        /// <inheritdoc />
        /// <summary>
        /// Save settings object
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="settings">Settings</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        public virtual void SaveSetting<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector,
            bool clearCache = true) where T : ISettings, new()
        {
            if (!(keySelector.Body is MemberExpression member))
            {
                throw new ArgumentException($"Expression '{keySelector}' refers to a method, not a property.");
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException($"Expression '{keySelector}' refers to a field, not a property.");
            }

            var key = settings.GetSettingKey(keySelector);
            //Duck typing is not supported in C#. That's why we're using dynamic type
            dynamic value = propInfo.GetValue(settings, null);
            SetSetting(key, value != null ? value : "", clearCache);
        }

        /// <inheritdoc />
        /// <summary>
        /// Delete all settings
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public virtual void DeleteSetting<T>() where T : ISettings, new()
        {
            var settingsToDelete = new List<Setting>();
            var allSettings = GetAllSettings();
            foreach (var prop in typeof(T).GetProperties())
            {
                var key = typeof(T).Name + "." + prop.Name;
                settingsToDelete.AddRange(allSettings.Where(x => x.Name.Equals(key, StringComparison.OrdinalIgnoreCase)));
            }

            foreach (var setting in settingsToDelete)
                DeleteSetting(setting);
        }

        /// <inheritdoc />
        /// <summary>
        /// Delete settings object
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="settings">Settings</param>
        /// <param name="keySelector">Key selector</param>
        public virtual void DeleteSetting<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
        {
            var key = settings.GetSettingKey(keySelector);
            key = key.Trim().ToLowerInvariant();

            var allSettings = GetAllSettingsCached();
            var settingForCaching = allSettings.ContainsKey(key) ?
                allSettings[key].FirstOrDefault() : null;
            if (settingForCaching == null)
            {
                return;
            }

            //update
            var setting = GetSettingById(settingForCaching.Id);
            DeleteSetting(setting);
        }

        /// <inheritdoc />
        /// <summary>
        /// Clear cache
        /// </summary>
        public virtual void ClearCache()
        {
            _cacheManager.RemoveByPattern(SettingsPatternKey);
        }

        #endregion
    }
}
