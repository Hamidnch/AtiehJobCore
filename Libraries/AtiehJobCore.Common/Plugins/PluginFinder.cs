﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AtiehJobCore.Common.Plugins
{
    /// <inheritdoc />
    /// <summary>
    /// Plugin finder
    /// </summary>
    public class PluginFinder : IPluginFinder
    {
        #region Fields

        private IList<PluginDescriptor> _plugins;
        private bool _arePluginsLoaded;

        #endregion

        #region Utilities

        /// <summary>
        /// Ensure plugins are loaded
        /// </summary>
        protected virtual void EnsurePluginsAreLoaded()
        {
            if (_arePluginsLoaded)
            {
                return;
            }

            var foundPlugins = PluginManager.ReferencedPlugins.ToList();
            foundPlugins.Sort();
            _plugins = foundPlugins.ToList();

            _arePluginsLoaded = true;
        }

        /// <summary>
        /// Check whether the plugin is available in a certain store
        /// </summary>
        /// <param name="pluginDescriptor">Plugin descriptor to check</param>
        /// <param name="loadMode">Load plugins mode</param>
        /// <returns>true - available; false - no</returns>
        protected virtual bool CheckLoadMode(PluginDescriptor pluginDescriptor, LoadPluginsMode loadMode)
        {
            if (pluginDescriptor == null)
                throw new ArgumentNullException(nameof(pluginDescriptor));

            switch (loadMode)
            {
                case LoadPluginsMode.All:
                    //no filtering
                    return true;
                case LoadPluginsMode.InstalledOnly:
                    return pluginDescriptor.Installed;
                case LoadPluginsMode.NotInstalledOnly:
                    return !pluginDescriptor.Installed;
                default:
                    throw new Exception("Not supported LoadPluginsMode");
            }
        }

        /// <summary>
        /// Check whether the plugin is in a certain group
        /// </summary>
        /// <param name="pluginDescriptor">Plugin descriptor to check</param>
        /// <param name="group">Group</param>
        /// <returns>true - available; false - no</returns>
        protected virtual bool CheckGroup(PluginDescriptor pluginDescriptor, string group)
        {
            if (pluginDescriptor == null)
                throw new ArgumentNullException(nameof(pluginDescriptor));

            return string.IsNullOrEmpty(group) || group.Equals(pluginDescriptor.Group, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Gets plugin groups
        /// </summary>
        /// <returns>Plugins groups</returns>
        public virtual IEnumerable<string> GetPluginGroups()
        {
            return GetPluginDescriptors(LoadPluginsMode.All).Select(x => x.Group).Distinct().OrderBy(x => x);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets plugins
        /// </summary>
        /// <typeparam name="T">The type of plugins to get.</typeparam>
        /// <param name="loadMode">Load plugins mode</param>
        /// <param name="group">Filter by plugin group; pass null to load all records</param>
        /// <returns>Plugins</returns>
        public virtual IEnumerable<T> GetPlugins<T>(LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly,
             string group = null) where T : class, IPlugin
        {
            return GetPluginDescriptors<T>(loadMode, group).Select(p => p.Instance<T>());
        }

        /// <inheritdoc />
        /// <summary>
        /// Get plugin descriptors
        /// </summary>
        /// <param name="loadMode">Load plugins mode</param>
        /// <param name="group">Filter by plugin group; pass null to load all records</param>
        /// <returns>Plugin descriptors</returns>
        public virtual IEnumerable<PluginDescriptor> GetPluginDescriptors(LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly,
             string group = null)
        {
            //ensure plugins are loaded
            EnsurePluginsAreLoaded();

            return _plugins.Where(p => CheckLoadMode(p, loadMode) && CheckGroup(p, group));
        }

        /// <inheritdoc />
        /// <summary>
        /// Get plugin descriptors
        /// </summary>
        /// <typeparam name="T">The type of plugin to get.</typeparam>
        /// <param name="loadMode">Load plugins mode</param>
        /// <param name="group">Filter by plugin group; pass null to load all records</param>
        /// <returns>Plugin descriptors</returns>
        public virtual IEnumerable<PluginDescriptor> GetPluginDescriptors<T>(LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly,
             string group = null)
            where T : class, IPlugin
        {
            return GetPluginDescriptors(loadMode, group)
                .Where(p => typeof(T).IsAssignableFrom(p.PluginType));
        }

        /// <inheritdoc />
        /// <summary>
        /// Get a plugin descriptor by its system name
        /// </summary>
        /// <param name="systemName">Plugin system name</param>
        /// <param name="loadMode">Load plugins mode</param>
        /// <returns>&gt;Plugin descriptor</returns>
        public virtual PluginDescriptor GetPluginDescriptorBySystemName(string systemName, LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly)
        {
            return GetPluginDescriptors(loadMode)
                .SingleOrDefault(p => p.SystemName.Equals(systemName, StringComparison.OrdinalIgnoreCase));
        }

        /// <inheritdoc />
        /// <summary>
        /// Get a plugin descriptor by its system name
        /// </summary>
        /// <typeparam name="T">The type of plugin to get.</typeparam>
        /// <param name="systemName">Plugin system name</param>
        /// <param name="loadMode">Load plugins mode</param>
        /// <returns>&gt;Plugin descriptor</returns>
        public virtual PluginDescriptor GetPluginDescriptorBySystemName<T>(string systemName, LoadPluginsMode loadMode = LoadPluginsMode.InstalledOnly)
            where T : class, IPlugin
        {
            return GetPluginDescriptors<T>(loadMode)
                .SingleOrDefault(p => p.SystemName.Equals(systemName, StringComparison.OrdinalIgnoreCase));
        }

        /// <inheritdoc />
        /// <summary>
        /// Reload plugins
        /// </summary>
        public virtual void ReloadPlugins()
        {
            _arePluginsLoaded = false;
            EnsurePluginsAreLoaded();
        }

        #endregion
    }
}
