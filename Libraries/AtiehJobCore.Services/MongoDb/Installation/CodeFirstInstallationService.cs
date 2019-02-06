using AtiehJobCore.Common;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.Enums.MongoDb;
using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.MongoDb;
using AtiehJobCore.Common.MongoDb.Data;
using AtiehJobCore.Common.MongoDb.Domain.Common;
using AtiehJobCore.Common.MongoDb.Domain.Localization;
using AtiehJobCore.Common.MongoDb.Domain.Security;
using AtiehJobCore.Common.MongoDb.Domain.Users;
using AtiehJobCore.Common.Utilities;
using AtiehJobCore.Data.MongoDb;
using AtiehJobCore.Services.Common;
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
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Language> _languageRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IGenericAttributeService _genericAttributeService;

        public CodeFirstInstallationService(IRepository<AtiehJobCoreVersion> versionRepository,
            IHostingEnvironment hostingEnvironment, IRepository<Language> languageRepository,
            IRepository<Role> roleRepository, IRepository<User> userRepository,
            IGenericAttributeService genericAttributeService)
        {
            _versionRepository = versionRepository;
            _hostingEnvironment = hostingEnvironment;
            _languageRepository = languageRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _genericAttributeService = genericAttributeService;
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
            InstallUsers(defaultUserEmail, defaultUserPassword);
        }

        protected virtual void InstallSettings(bool installSampleData)
        {
            var settingService = EngineContext.Current.Resolve<ISettingService>();
            settingService.SaveSetting(new AdminAreaSettings
            {
                DefaultGridPageSize = 15,
                GridPageSizes = "10, 15, 20, 50, 100",
                RichEditorAdditionalSettings = null,
                RichEditorAllowJavaScript = false,
                UseIsoDateTimeConverterInJson = true,
                AdminLayout = "Darkblue",
                KendoLayout = "custom",
            });
            settingService.SaveSetting(new SecuritySettings
            {
                EncryptionKey = CommonHelper.GenerateRandomDigitCode(24),
                AdminAreaAllowedIpAddresses = null,
                EnableXsrfProtectionForAdminArea = true,
                EnableXsrfProtection = true,
                HoneypotEnabled = false,
                HoneypotInputName = "hpinput",
                AllowNonAsciiCharInHeaders = true,
            });
            settingService.SaveSetting(new UserSettings
            {
                UserLoginType = UserLoginType.Email,
                CheckUsernameAvailabilityEnabled = false,
                AllowUsersToChangeUsernames = false,
                DefaultPasswordFormat = PasswordFormat.Clear,
                HashedPasswordFormat = "SHA1",
                PasswordMinLength = 6,
                PasswordRecoveryLinkDaysValid = 7,
                PasswordLifetime = 90,
                FailedPasswordAllowedAttempts = 0,
                FailedPasswordLockoutMinutes = 30,
                UserRegistrationType = UserRegistrationType.Standard,
                AllowUsersToUploadAvatars = false,
                AvatarMaximumSizeBytes = 20000,
                DefaultAvatarEnabled = true,
                ShowUsersLocation = false,
                ShowUsersJoinDate = false,
                AllowViewingProfiles = false,
                NotifyNewUserRegistration = false,
                HideDownloadableProductsTab = false,
                UserNameFormat = UserNameFormat.ShowFirstName,
                GenderEnabled = true,
                DateOfBirthEnabled = true,
                DateOfBirthRequired = false,
                DateOfBirthMinimumAge = 0,
                CompanyEnabled = true,
                StreetAddressEnabled = false,
                StreetAddress2Enabled = false,
                ZipPostalCodeEnabled = false,
                CityEnabled = false,
                CountryEnabled = false,
                CountryRequired = false,
                StateProvinceEnabled = false,
                StateProvinceRequired = false,
                PhoneEnabled = false,
                FaxEnabled = false,
                AcceptPrivacyPolicyEnabled = false,
                NewsletterEnabled = true,
                NewsletterTickedByDefault = true,
                HideNewsletterBlock = false,
                NewsletterBlockAllowToUnsubscribe = false,
                OnlineUserMinutes = 20,
                StoreLastVisitedPage = false,
                SaveVisitedPage = false,
                SuffixDeletedUsers = true,
                AllowUsersToDeleteAccount = false,
                AllowUsersToExportData = false
            });
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

            settingService.SaveSetting(new CommonSettings
            {
                StoreInDatabaseContactUsForm = true,
                UseSystemEmailForContactUsForm = true,
                UseStoredProceduresIfSupported = true,
                SitemapEnabled = true,
                SitemapIncludeCategories = true,
                SitemapIncludeManufacturers = true,
                SitemapIncludeProducts = false,
                DisplayJavaScriptDisabledWarning = false,
                UseFullTextSearch = false,
                FullTextMode = FulltextSearchMode.ExactMatch,
                Log404Errors = true,
                BreadcrumbDelimiter = "/",
                RenderXuaCompatible = false,
                XuaCompatibleValue = "IE=edge",
                DeleteGuestTaskOlderThanMinutes = 1440,
                PopupForTermsOfServiceLinks = true
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
            var enLanguage = new Language
            {
                Name = "English",
                LanguageCulture = "en-US",
                UniqueSeoCode = "en",
                FlagImageFileName = "us.png",
                Published = true,
                DisplayOrder = 0
            };
            _languageRepository.Insert(enLanguage);

            var faLanguage = new Language
            {
                Name = "Persian",
                LanguageCulture = "fa-IR",
                UniqueSeoCode = "fa",
                FlagImageFileName = "ir.png",
                Published = true,
                DisplayOrder = 1,
                Rtl = true
            };
            _languageRepository.Insert(faLanguage);
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
        protected virtual void InstallUsers(string defaultUserEmail, string defaultUserPassword)
        {
            var crAdministrators = new Role
            {
                Name = "Administrators",
                Active = true,
                IsSystemRole = true,
                SystemName = SystemUserRoleNames.Administrators,
            };
            _roleRepository.Insert(crAdministrators);


            var crRegistered = new Role
            {
                Name = "Registered",
                Active = true,
                IsSystemRole = true,
                SystemName = SystemUserRoleNames.Registered,
            };
            _roleRepository.Insert(crRegistered);

            var crGuests = new Role
            {
                Name = "Guests",
                Active = true,
                IsSystemRole = true,
                SystemName = SystemUserRoleNames.Guests,
            };
            _roleRepository.Insert(crGuests);

            //admin user
            var adminUser = new User
            {
                UserGuid = Guid.NewGuid(),
                Email = defaultUserEmail,
                Username = defaultUserEmail,
                Password = defaultUserPassword,
                PasswordFormat = PasswordFormat.Clear,
                PasswordSalt = "",
                Active = true,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow,
                PasswordChangeDateUtc = DateTime.UtcNow,
            };

            adminUser.Roles.Add(crAdministrators);
            adminUser.Roles.Add(crRegistered);
            _userRepository.Insert(adminUser);

            //set default user name
            _genericAttributeService.SaveAttribute(adminUser, SystemUserAttributeNames.FirstName, "Hamid");
            _genericAttributeService.SaveAttribute(adminUser, SystemUserAttributeNames.LastName, "Nch");


            //search engine (crawler) built-in user
            var searchEngineUser = new User
            {
                Email = "builtin@search_engine_record.com",
                UserGuid = Guid.NewGuid(),
                PasswordFormat = PasswordFormat.Clear,
                AdminComment = "Built-in system guest record used for requests from search engines.",
                Active = true,
                IsSystemAccount = true,
                SystemName = SystemUserNames.SearchEngine,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow,
            };
            searchEngineUser.Roles.Add(crGuests);
            _userRepository.Insert(searchEngineUser);


            //built-in user for background tasks
            var backgroundTaskUser = new User
            {
                Email = "builtin@background-task-record.com",
                UserGuid = Guid.NewGuid(),
                PasswordFormat = PasswordFormat.Clear,
                AdminComment = "Built-in system record used for background tasks.",
                Active = true,
                IsSystemAccount = true,
                SystemName = SystemUserNames.BackgroundTask,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow,
            };
            backgroundTaskUser.Roles.Add(crGuests);
            _userRepository.Insert(backgroundTaskUser);
        }
    }
}
