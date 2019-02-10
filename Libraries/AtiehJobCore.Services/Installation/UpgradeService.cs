using AtiehJobCore.Core.Domain.Common;
using AtiehJobCore.Core.Domain.Localization;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Core.Utilities;
using AtiehJobCore.Services.Localization;
using System.IO;
using System.Linq;

namespace AtiehJobCore.Services.Installation
{
    public partial class UpgradeService : IUpgradeService
    {
        #region Fields

        private readonly IRepository<AtiehJobCoreVersion> _versionRepository;
        //private readonly ILocalizationService _localizationService;

        private const string Version100 = "1.00";
        private const string Version110 = "1.10";

        #endregion

        #region Ctor
        public UpgradeService(IRepository<AtiehJobCoreVersion> versionRepository
            //, ILocalizationService localizationService
            )
        {
            this._versionRepository = versionRepository;
            //_localizationService = localizationService;
        }
        #endregion

        public virtual string DatabaseVersion()
        {
            var version = Version100;
            var databaseVersion = _versionRepository.Table.FirstOrDefault();
            if (databaseVersion != null)
                version = databaseVersion.DataBaseVersion;
            return version;
        }
        public virtual void UpgradeData(string fromVersion, string toVersion)
        {
            if (fromVersion == Version100)
            {
                From100To110();
                fromVersion = Version110;
            }


            if (fromVersion != toVersion)
            {
                return;
            }

            var databaseVersion = _versionRepository.Table.FirstOrDefault();
            if (databaseVersion != null)
            {
                databaseVersion.DataBaseVersion = SiteVersion.CurrentVersion;
                _versionRepository.Update(databaseVersion);
            }
            else
            {
                databaseVersion = new AtiehJobCoreVersion
                {
                    DataBaseVersion = SiteVersion.CurrentVersion
                };
                _versionRepository.Insert(databaseVersion);
            }
        }

        private void From100To110()
        {
            #region Install String resources
            InstallStringResources("100_110.res.xml");
            #endregion


            #region Settings

            EngineContext.Current.Resolve<IRepository<Setting>>().Insert(new Setting() { Name = "CommonSettings.SitemapCustomUrls", Value = "" });

            #endregion
        }

        private void InstallStringResources(string fileNames)
        {
            //'English' language            
            var language = EngineContext.Current.Resolve<IRepository<Language>>().Table.Single(l => l.Name == "English");

            //save resources
            foreach (var filePath in Directory.EnumerateFiles(
                CommonHelper.MapPath("~/App_Data/Localization/Upgrade"), "*" + fileNames, SearchOption.TopDirectoryOnly))
            {
                var localesXml = File.ReadAllText(filePath);
                var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
                localizationService.ImportResourcesFromXmlInstall(language, localesXml);
            }
        }
    }
}
