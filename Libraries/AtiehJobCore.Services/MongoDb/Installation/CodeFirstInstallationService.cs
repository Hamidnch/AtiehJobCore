using AtiehJobCore.Common;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.MongoDb;
using AtiehJobCore.Common.MongoDb.Data;
using AtiehJobCore.Common.MongoDb.Domain.Common;
using AtiehJobCore.Common.MongoDb.Domain.Localization;
using AtiehJobCore.Common.Utilities;
using AtiehJobCore.Data.MongoDb;
using AtiehJobCore.Services.MongoDb.Configuration;
using AtiehJobCore.Services.MongoDb.Localization;
using Microsoft.AspNetCore.Hosting;
using MongoDB.Driver;
using System;
using System.IO;
using System.Linq;

namespace AtiehJobCore.Services.MongoDb.Installation
{
    public class CodeFirstInstallationService : IInstallationService
    {
        private readonly IRepository<AtiehJobCoreVersion> _versionRepository;
        private readonly IRepository<Language> _languageRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CodeFirstInstallationService(IRepository<AtiehJobCoreVersion> versionRepository,
            IHostingEnvironment hostingEnvironment, IRepository<Language> languageRepository)
        {
            _versionRepository = versionRepository;
            _hostingEnvironment = hostingEnvironment;
            _languageRepository = languageRepository;
        }

        protected virtual string GetSamplesPath()
        {
            return Path.Combine(_hostingEnvironment.WebRootPath, "content/samples/");
        }
        private static void CreateTables(string local)
        {
            if (string.IsNullOrEmpty(local))
                local = "en";

            try
            {
                var options = new CreateCollectionOptions();
                var collation = new Collation(local);
                options.Collation = collation;
                var dataSettingsManager = new DataSettingsManager();
                var connectionString = dataSettingsManager.LoadSettings().DataConnectionString;
                var mongoDbContext = new MongoDbContext(connectionString);
                var typeFinder = EngineContext.Current.Resolve<ITypeFinder>();
                var q = typeFinder.GetAssemblies().FirstOrDefault(x => x.GetName().Name == "AtiehJobCore.Common");
                foreach (var item in q.GetTypes().Where(x => x.Namespace != null && x.Namespace.StartsWith("AtiehJobCore.Common.MongoDb")))
                {
                    if (item.BaseType == null)
                    {
                        continue;
                    }

                    if (item.IsClass && item.BaseType == typeof(BaseMongoEntity))
                        mongoDbContext.Database().CreateCollection(item.Name, options);
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }
        private void CreateIndexes()
        {
            var indexOptionId = new CreateIndexOptions
            {
                Name = "db",
                Unique = true
            };
            var atiehJobVersionIndex =
                new CreateIndexModel<AtiehJobCoreVersion>(
                    (Builders<AtiehJobCoreVersion>.IndexKeys.Ascending(x => x.DataBaseVersion)), indexOptionId);

            _versionRepository.Collection.Indexes.CreateOne(atiehJobVersionIndex);
        }
        public void InstallData(string defaultUserEmail, string defaultUserPassword, string collation, bool installSampleData = true)
        {
            defaultUserEmail = defaultUserEmail.ToLower();
            CreateTables(collation);
            CreateIndexes();
            InstallVersion();
            InstallLanguages();
            InstallLocaleResources();
            InstallSettings(installSampleData);
        }

        protected virtual void InstallSettings(bool installSampleData)
        {
            var settingService = EngineContext.Current.Resolve<ISettingService>();

            settingService.SaveSetting(new LocalizationSettings
            {
                DefaultAdminLanguageId = _languageRepository.Table.Single(l => l.Name == "English").Id,
                UseImagesForLanguageSelection = false,
                SeoFriendlyUrlsForLanguagesEnabled = false,
                AutomaticallyDetectLanguage = false,
                LoadAllLocaleRecordsOnStartup = true,
                LoadAllLocalizedPropertiesOnStartup = true,
                LoadAllUrlRecordsOnStartup = false,
                IgnoreRtlPropertyForAdminArea = false,
            });
        }
        protected virtual void InstallVersion()
        {
            var version = new AtiehJobCoreVersion
            {
                DataBaseVersion = SiteVersion.CurrentVersion
            };
            _versionRepository.Insert(version);
        }
        protected virtual void InstallLanguages()
        {
            var language = new Language
            {
                Name = "English",
                LanguageCulture = "en-US",
                UniqueSeoCode = "en",
                FlagImageFileName = "us.png",
                Published = true,
                DisplayOrder = 1
            };
            _languageRepository.Insert(language);
        }
        protected virtual void InstallLocaleResources()
        {
            //'English' language
            var language = _languageRepository.Table.Single(l => l.Name == "English");

            //save resources
            foreach (var filePath in Directory.EnumerateFiles(
                CommonHelper.MapPath("~/App_Data/Localization/"), "*.atiehjobres.xml", SearchOption.TopDirectoryOnly))
            {
                var localesXml = File.ReadAllText(filePath);
                var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
                localizationService.ImportResourcesFromXmlInstall(language, localesXml);
            }

        }
    }
}
