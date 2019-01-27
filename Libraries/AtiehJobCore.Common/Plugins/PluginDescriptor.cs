using AtiehJobCore.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AtiehJobCore.Common.Plugins
{
    public class PluginDescriptor : IDescriptor, IComparable<PluginDescriptor>
    {
        public PluginDescriptor()
        {
            this.SupportedVersions = new List<string>();
            this.LimitedToStores = new List<string>();
        }


        public PluginDescriptor(Assembly referencedAssembly, FileInfo originalAssemblyFile,
            Type pluginType)
            : this()
        {
            this.ReferencedAssembly = referencedAssembly;
            this.OriginalAssemblyFile = originalAssemblyFile;
            this.PluginType = pluginType;
        }
        /// <summary>
        /// Plugin type
        /// </summary>
        public virtual string PluginFileName { get; set; }

        /// <summary>
        /// Plugin type
        /// </summary>
        public virtual Type PluginType { get; set; }

        /// <summary>
        /// The assembly that has been shadow copied that is active in the application
        /// </summary>
        public virtual Assembly ReferencedAssembly { get; internal set; }

        /// <summary>
        /// The original assembly file that a shadow copy was made from it
        /// </summary>
        public virtual FileInfo OriginalAssemblyFile { get; internal set; }

        /// <summary>
        /// Gets or sets the plugin group
        /// </summary>
        public virtual string Group { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the friendly name
        /// </summary>
        public virtual string FriendlyName { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the system name
        /// </summary>
        public virtual string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the version
        /// </summary>
        public virtual string Version { get; set; }

        /// <summary>
        /// Gets or sets the supported versions 
        /// </summary>
        public virtual IList<string> SupportedVersions { get; set; }

        /// <summary>
        /// Gets or sets the author
        /// </summary>
        public virtual string Author { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public virtual int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the list of store identifiers in which this plugin is available. If empty, then this plugin is available in all stores
        /// </summary>
        public virtual IList<string> LimitedToStores { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether plugin is installed
        /// </summary>
        public virtual bool Installed { get; set; }

        public virtual T Instance<T>() where T : class, IPlugin
        {
            object instance = null;
            try
            {
                instance = EngineContext.Current.Resolve(PluginType);
            }
            catch
            {
                //try resolve
            }
            if (instance == null)
            {
                //not resolved
                instance = EngineContext.Current.ResolveUnregistered(PluginType);
            }
            var typedInstance = instance as T;
            if (typedInstance != null)
                typedInstance.PluginDescriptor = this;
            return typedInstance;
        }

        public IPlugin Instance()
        {
            return Instance<IPlugin>();
        }

        public int CompareTo(PluginDescriptor other)
        {
            return DisplayOrder != other.DisplayOrder
                ? DisplayOrder.CompareTo(other.DisplayOrder)
                : string.Compare(FriendlyName, other.FriendlyName, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return FriendlyName;
        }

        public override bool Equals(object obj)
        {
            return obj is PluginDescriptor other &&
                SystemName != null &&
                SystemName.Equals(other.SystemName);
        }

        public override int GetHashCode()
        {
            return SystemName.GetHashCode();
        }
    }
}
