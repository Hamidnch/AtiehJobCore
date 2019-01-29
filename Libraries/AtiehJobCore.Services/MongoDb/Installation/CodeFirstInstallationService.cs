using System;
using System.IO;
using System.Linq;
using AtiehJobCore.Common;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.MongoDb.Common;
using AtiehJobCore.Common.MongoDb.Data;
using AtiehJobCore.Data.MongoDb;
using Microsoft.AspNetCore.Hosting;
using MongoDB.Driver;

namespace AtiehJobCore.Services.MongoDb.Installation
{
    public class CodeFirstInstallationService : IInstallationService
    {
        private readonly IRepository<AtiehJobCoreVersion> _versionRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CodeFirstInstallationService(IRepository<AtiehJobCoreVersion> versionRepository,
            IHostingEnvironment hostingEnvironment)
        {
            _versionRepository = versionRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public void InstallData(string defaultUserEmail, string defaultUserPassword, string collation, bool installSampleData = true)
        {
            defaultUserEmail = defaultUserEmail.ToLower();
            CreateTables(collation);
            CreateIndexes();
            InstallVersion();
        }

        private void CreateTables(string local)
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


        protected virtual string GetSamplesPath()
        {
            return Path.Combine(_hostingEnvironment.WebRootPath, "content/samples/");
        }


        protected virtual void InstallVersion()
        {
            var version = new AtiehJobCoreVersion
            {
                DataBaseVersion = SiteVersion.CurrentVersion
            };
            _versionRepository.Insert(version);
        }
    }
}
