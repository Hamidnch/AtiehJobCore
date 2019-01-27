﻿using System.Collections.Generic;
using System.Reflection;

namespace AtiehJobCore.Common.Infrastructure
{
    /// <inheritdoc />
    /// <summary>
    /// Provides information about types in the current web application. 
    /// Optionally this class can look at all assemblies in the bin folder.
    /// </summary>
    public class WebAppTypeFinder : AppDomainTypeFinder
    {
        #region Fields

        private bool _binFolderAssembliesLoaded;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets wether assemblies in the bin folder of the web application
        /// should be specificly checked for being loaded on application load.
        /// This is need in situations where plugins need to be loaded in the AppDomain after the application been reloaded.
        /// </summary>
        public bool EnsureBinFolderAssembliesLoaded { get; set; } = true;

        #endregion

        #region Methods

        /// <summary>
        /// Gets a physical disk path of \Bin directory
        /// </summary>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        public virtual string GetBinDirectory()
        {
            return System.AppContext.BaseDirectory;
        }

        public override IList<Assembly> GetAssemblies()
        {
            if (!this.EnsureBinFolderAssembliesLoaded || _binFolderAssembliesLoaded)
            {
                return base.GetAssemblies();
            }

            _binFolderAssembliesLoaded = true;
            var binPath = GetBinDirectory();
            LoadMatchingAssemblies(binPath);
            return base.GetAssemblies();
        }

        #endregion
    }
}
