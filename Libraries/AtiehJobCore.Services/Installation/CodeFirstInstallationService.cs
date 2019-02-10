using AtiehJobCore.Core;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain;
using AtiehJobCore.Core.Domain.Common;
using AtiehJobCore.Core.Domain.Localization;
using AtiehJobCore.Core.Domain.Security;
using AtiehJobCore.Core.Domain.Seo;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Core.MongoDb;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Core.Utilities;
using AtiehJobCore.Data.Context;
using AtiehJobCore.Services.Common;
using AtiehJobCore.Services.Configuration;
using AtiehJobCore.Services.Localization;
using Microsoft.AspNetCore.Hosting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AtiehJobCore.Services.Installation
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
                var q = typeFinder.GetAssemblies().FirstOrDefault(x => x.GetName().Name == "AtiehJobCore.Core");
                foreach (var item in q.GetTypes().Where(x => x.Namespace != null && x.Namespace.StartsWith("AtiehJobCore.Core.Domain")))
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
                throw new AtiehJobException(ex.Message);
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
            settingService.SaveSetting(new SeoSettings
            {
                PageTitleSeparator = ". ",
                PageTitleSeoAdjustment = PageTitleSeoAdjustment.PagenameAfterSiteName,
                DefaultTitle = "آتیه کار",
                DefaultMetaKeywords = "",
                DefaultMetaDescription = "",
                GenerateProductMetaDescription = true,
                ConvertNonWesternChars = false,
                AllowUnicodeCharsInUrls = true,
                CanonicalUrlsEnabled = false,
                WwwRequirement = WwwRequirement.NoMatter,
                //we disable bundling out of the box because it requires a lot of server resources
                EnableJsBundling = false,
                EnableCssBundling = false,
                TwitterMetaTags = true,
                OpenGraphMetaTags = true,
                ReservedUrlRecordSlugs = new List<string>
                    {
                        "admin",
                        "install",
                        "login",
                        "register",
                        "logout",
                        "contactus",
                        "passwordrecovery",
                        "news",
                        "sitemap",
                        "search",
                        "config",
                        "eucookielawaccept",
                        "page-not-found",
                        //system names are not allowed (anyway they will cause a runtime error),
                        "con",
                        "lpt1",
                        "lpt2",
                        "lpt3",
                        "lpt4",
                        "lpt5",
                        "lpt6",
                        "lpt7",
                        "lpt8",
                        "lpt9",
                        "com1",
                        "com2",
                        "com3",
                        "com4",
                        "com5",
                        "com6",
                        "com7",
                        "com8",
                        "com9",
                        "null",
                        "prn",
                        "aux"
                    },
            });
            settingService.SaveSetting(new ExternalAuthenticationSettings
            {
                AutoRegisterEnabled = true,
                RequireEmailValidation = false
            });
            settingService.SaveSetting(new StoreInformationSettings
            {
                SiteClosed = false,
                DefaultStoreTheme = "DefaultClean",
                AllowCustomerToSelectTheme = false,
                DisplayMiniProfilerInPublicStore = false,
                DisplayEuCookieLawWarning = false,
                FacebookLink = "https://www.facebook.com/atiehjobcom",
                TwitterLink = "https://twitter.com/atiehjob",
                YoutubeLink = "http://www.youtube.com/user/atiehjob",
                GooglePlusLink = "https://plus.google.com/atiehjob",
                InstagramLink = "https://www.instagram.com/atiehjob/",
                LinkedInLink = "https://www.linkedin.com/company/atiehjob.ir/",
                PinterestLink = "",
                HidePoweredByAtiehJob = false
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
