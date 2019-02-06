namespace AtiehJobCore.Common.Plugins
{
    public abstract class BasePlugin : IPlugin
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        /// <returns></returns>
        public virtual string GetConfigurationPageUrl()
        {
            return null;
        }
        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the plugin descriptor
        /// </summary>
        public virtual PluginDescriptor PluginDescriptor { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Install plugin
        /// </summary>
        public virtual void Install()
        {
            PluginManager.MarkPluginAsInstalled(this.PluginDescriptor.SystemName);
        }

        /// <inheritdoc />
        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public virtual void Uninstall()
        {
            PluginManager.MarkPluginAsUninstalled(this.PluginDescriptor.SystemName);
        }

    }
}
