using AtiehJobCore.Core;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain;
using AtiehJobCore.Core.Domain.Catalog;
using AtiehJobCore.Core.Domain.Common;
using AtiehJobCore.Core.Domain.Employers;
using AtiehJobCore.Core.Domain.Localization;
using AtiehJobCore.Core.Domain.Logging;
using AtiehJobCore.Core.Domain.News;
using AtiehJobCore.Core.Domain.Placements;
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
using AtiehJobCore.Services.Helpers;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Services.Users;
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
        #region Fields
        private readonly IRepository<AtiehJobVersion> _versionRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Language> _languageRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IRepository<ActivityLogType> _activityLogTypeRepository;
        private readonly IRepository<LocaleStringResource> _lsrRepository;
        private readonly IRepository<UrlRecord> _urlRecordRepository;
        private readonly IRepository<NewsItem> _newsItemRepository;
        private readonly IRepository<Log> _logRepository;
        private readonly IRepository<UserHistoryPassword> _userHistoryPasswordRepository;
        private readonly IRepository<UserNote> _userNoteRepository;
        private readonly IRepository<UserApi> _userApiRepository;
        private readonly IRepository<Setting> _settingRepository;
        private readonly IRepository<PermissionRecord> _permissionRepository;
        private readonly IRepository<ExternalAuthenticationRecord> _externalAuthenticationRepository;
        #endregion Fields

        #region Ctor
        public CodeFirstInstallationService(IRepository<AtiehJobVersion> versionRepository,
            IHostingEnvironment hostingEnvironment, IRepository<Language> languageRepository,
            IRepository<Role> roleRepository, IRepository<User> userRepository,
            IGenericAttributeService genericAttributeService,
            IRepository<ActivityLogType> activityLogTypeRepository,
            IRepository<LocaleStringResource> lsrRepository,
            IRepository<UserHistoryPassword> userHistoryPasswordRepository,
            IRepository<UserNote> userNoteRepository, IRepository<UserApi> userApiRepository,
            IRepository<UrlRecord> urlRecordRepository, IRepository<NewsItem> newsItemRepository,
            IRepository<Log> logRepository, IRepository<Setting> settingRepository,
            IRepository<PermissionRecord> permissionRepository,
            IRepository<ExternalAuthenticationRecord> externalAuthenticationRepository)
        {
            _versionRepository = versionRepository;
            _hostingEnvironment = hostingEnvironment;
            _languageRepository = languageRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _genericAttributeService = genericAttributeService;
            _activityLogTypeRepository = activityLogTypeRepository;
            _lsrRepository = lsrRepository;
            _userHistoryPasswordRepository = userHistoryPasswordRepository;
            _userNoteRepository = userNoteRepository;
            _userApiRepository = userApiRepository;
            _urlRecordRepository = urlRecordRepository;
            _newsItemRepository = newsItemRepository;
            _logRepository = logRepository;
            _settingRepository = settingRepository;
            _permissionRepository = permissionRepository;
            _externalAuthenticationRepository = externalAuthenticationRepository;
        }
        #endregion Ctor

        protected virtual string GetSamplesPath()
        {
            return Path.Combine(_hostingEnvironment.WebRootPath, "content/samples/");
        }
        private static void CreateTables(string local)
        {
            if (string.IsNullOrEmpty(local))
                local = "fa"; //en

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
                if (q == null)
                {
                    return;
                }

                {
                    foreach (var item in q.GetTypes().Where(x =>
                        x.Namespace != null && x.Namespace.StartsWith("AtiehJobCore.Core.Domain")))
                    {
                        if (item.BaseType == null)
                        {
                            continue;
                        }

                        if (item.IsClass && item.BaseType == typeof(BaseMongoEntity))
                            mongoDbContext.Database().CreateCollection(item.Name, options);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new AtiehJobException(ex.Message);
            }
        }
        private void CreateIndexes()
        {
            _versionRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<AtiehJobVersion>(
                (Builders<AtiehJobVersion>.IndexKeys.Ascending(x => x.DataBaseVersion)),
                new CreateIndexOptions() { Name = "Version", Unique = true }));

            //Language
            _lsrRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<LocaleStringResource>((Builders<LocaleStringResource>
                .IndexKeys.Ascending(x => x.LanguageId)
                .Ascending(x => x.ResourceName)), new CreateIndexOptions() { Name = "Language" }));

            _lsrRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<LocaleStringResource>((Builders<LocaleStringResource>
                    .IndexKeys.Ascending(x => x.ResourceName)), new CreateIndexOptions() { Name = "ResourceName" }));

            //user
            _userRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<User>((Builders<User>
                    .IndexKeys.Descending(x => x.CreatedOnUtc)
                    .Ascending(x => x.Deleted).Ascending("Roles._id")),
                    new CreateIndexOptions() { Name = "CreatedOnUtc_1_Roles._id_1", Unique = false }));

            _userRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<User>((Builders<User>
                    .IndexKeys.Ascending(x => x.LastActivityDateUtc)),
                    new CreateIndexOptions() { Name = "LastActivityDateUtc_1", Unique = false }));

            _userRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<User>((Builders<User>
                    .IndexKeys.Ascending(x => x.UserGuid)),
                    new CreateIndexOptions() { Name = "UserGuid_1", Unique = false }));

            _userRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<User>((Builders<User>
                    .IndexKeys.Ascending(x => x.Email)),
                    new CreateIndexOptions() { Name = "Email_1", Unique = false }));

            //user history password
            _userHistoryPasswordRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<UserHistoryPassword>(
                    (Builders<UserHistoryPassword>.IndexKeys.Ascending(x => x.UserId).Descending(x => x.CreatedOnUtc)),
                    new CreateIndexOptions() { Name = "UserId", Unique = false }));

            //user note
            _userNoteRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<UserNote>(
                    (Builders<UserNote>.IndexKeys.Ascending(x => x.UserId).Descending(x => x.CreatedOnUtc)),
                    new CreateIndexOptions() { Name = "UserId", Unique = false, Background = true }));

            //user api
            _userApiRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<UserApi>((Builders<UserApi>.IndexKeys.Ascending(x => x.Email)),
                    new CreateIndexOptions() { Name = "Email", Unique = true, Background = true }));

            //url record
            _urlRecordRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<UrlRecord>
                    ((Builders<UrlRecord>.IndexKeys.Ascending(x => x.Slug).Ascending(x => x.IsActive)),
                    new CreateIndexOptions() { Name = "Slug" }));
            _urlRecordRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<UrlRecord>(
                    (Builders<UrlRecord>.IndexKeys.Ascending(x => x.EntityId)
                        .Ascending(x => x.EntityName).Ascending(x => x.LanguageId).Ascending(x => x.IsActive)),
                    new CreateIndexOptions() { Name = "UrlRecord" }));

            //news
            _newsItemRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<NewsItem>(
                    (Builders<NewsItem>.IndexKeys.Descending(x => x.CreatedOnUtc)),
                    new CreateIndexOptions() { Name = "CreatedOnUtc", Unique = false }));

            //Log
            _logRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<Log>(
                    (Builders<Log>.IndexKeys.Descending(x => x.CreatedOnUtc)),
                    new CreateIndexOptions() { Name = "CreatedOnUtc", Unique = false }));

            //setting
            _settingRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<Setting>(
                    (Builders<Setting>.IndexKeys.Ascending(x => x.Name)),
                    new CreateIndexOptions() { Name = "Name", Unique = false }));

            //permission
            _permissionRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<PermissionRecord>(
                    (Builders<PermissionRecord>.IndexKeys.Ascending(x => x.SystemName)),
                    new CreateIndexOptions() { Name = "SystemName", Unique = true }));

            //externalAuth
            _externalAuthenticationRepository.Collection.Indexes
                .CreateOne(new CreateIndexModel<ExternalAuthenticationRecord>(
                    (Builders<ExternalAuthenticationRecord>.IndexKeys.Ascending(x => x.UserId)),
                    new CreateIndexOptions() { Name = "UserId" }));
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
            HashDefaultUserPassword(defaultUserEmail, defaultUserPassword);
            InstallActivityLogTypes();
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
                AllowNonAsciiCharInHeaders = true
            });
            settingService.SaveSetting(new UserSettings
            {
                UsernamesEnabled = false,
                CheckUsernameAvailabilityEnabled = false,
                AllowUsersToChangeUsernames = false,

                IsDisplayEmail = true,
                IsOptionalEmail = false,
                ForceEmailValidation = true,
                AllowDuplicateEmail = false,

                IsDisplayMobileNumber = true,
                IsOptionalMobileNumber = false,
                ForceMobileNumberValidation = true,
                AllowDuplicateMobileNumber = false,

                IsDisplayNationalCode = true,
                IsOptionalNationalCode = false,
                ForceNationalCodeValidation = true,
                AllowDuplicateNationalCode = false,

                IsSendRegisterationSms = true,
                IsSendRegisterationEmail = true,
                IsSendPasswordSms = true,
                IsSendPasswordEmail = true,

                IsAgreement = true,

                IsDisplayPassword = true,
                DefaultPasswordFormat = PasswordFormat.Hashed,
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
            settingService.SaveSetting(new EmployerSettings
            {
                IsDisplayInsuranceCode = true,
                IsOptionalInsuranceCode = false,
                AllowDuplicateInsuranceCode = false
            });
            settingService.SaveSetting(new PlacementSettings
            {
                IsDisplayLicenseNumber = true,
                IsOptionalLicenseNumber = false,
                AllowDuplicateLicenseNumber = false
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
                DefaultSiteTheme = "DefaultClean",
                AllowUserToSelectTheme = false,
                DisplayMiniProfilerInPublicStore = false,
                DisplayEuCookieLawWarning = true,
                FacebookLink = "https://www.facebook.com/atiehjobcom",
                TwitterLink = "https://twitter.com/atiehjob",
                YoutubeLink = "http://www.youtube.com/user/atiehjob",
                GooglePlusLink = "https://plus.google.com/atiehjob",
                InstagramLink = "https://www.instagram.com/atiehjob/",
                LinkedInLink = "https://www.linkedin.com/company/atiehjob.ir/",
                PinterestLink = "",
                HidePoweredByAtiehJob = false
            });
            settingService.SaveSetting(new CatalogSettings
            {
                DefaultViewMode = "grid",
                ShowShareButton = false,
                PageShareCode = "<!-- AddThis Button BEGIN --><div class=\"addthis_inline_share_toolbox\"></div><script type=\"text/javascript\" src=\"//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-5bbf4b026e74abf6\"></script><!-- AddThis Button END -->",
                EmailAFriendEnabled = true,
                AskQuestionEnabled = false,
                AllowAnonymousUsersToEmailAFriend = false,
                SearchPageProductsPerPage = 6,
                SearchPageAllowUsersToSelectPageSize = true,
                SearchPagePageSizeOptions = "6, 3, 9, 18",
                AjaxProcessAttributeChange = true,
                IgnoreAcl = true,
                IgnoreFilterableAvailableStartEndDateTime = true
            });
            settingService.SaveSetting(new DateTimeSettings
            {
                DefaultStoreTimeZoneId = "",
                AllowUsersToSetTimeZone = false
            });
            settingService.SaveSetting(new NewsSettings
            {
                Enabled = true,
                AllowNotRegisteredUsersToLeaveComments = true,
                NotifyAboutNewNewsComments = false,
                ShowNewsOnMainPage = true,
                MainPageNewsCount = 3,
                NewsArchivePageSize = 10,
                ShowHeaderRssUrl = false,
            });
        }
        protected virtual void InstallVersion()
        {
            var version = new AtiehJobVersion
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
            //var englishLanguage = _languageRepository.Table.Single(l => l.Name == "English");

            //save resources
            foreach (var filePath in Directory.EnumerateFiles(
                CommonHelper.MapPath("~/App_Data/Localization/"), "*.atiehjobres.xml", SearchOption.TopDirectoryOnly))
            {
                var localesXml = File.ReadAllText(filePath);
                var localizationService = EngineContext.Current.Resolve<ILocalizationService>();

                var langName = localizationService.GetLanguageFromXml(localesXml);
                if (langName == null)
                    continue;

                var lang = _languageRepository.Table.SingleOrDefault(l => l.Name == langName);

                localizationService.ImportResourcesFromXmlInstall(lang, localesXml);
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

            var jobSeekerRegistered = new Role
            {
                Name = "Jobseeker",
                Active = true,
                IsSystemRole = true,
                SystemName = SystemUserRoleNames.Jobseeker,
            };
            _roleRepository.Insert(jobSeekerRegistered);

            var employerRegistered = new Role
            {
                Name = "Employer",
                Active = true,
                IsSystemRole = true,
                SystemName = SystemUserRoleNames.Employer,
            };
            _roleRepository.Insert(employerRegistered);

            var placementRegistered = new Role
            {
                Name = "Placement",
                Active = true,
                IsSystemRole = true,
                SystemName = SystemUserRoleNames.Placement,
            };
            _roleRepository.Insert(placementRegistered);

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
        protected virtual void HashDefaultUserPassword(string defaultUserEmail, string defaultUserPassword)
        {
            var userRegistrationService = EngineContext.Current.Resolve<IUserRegistrationService>();
            userRegistrationService.ChangePassword(new ChangePasswordRequest(defaultUserEmail, false,
                PasswordFormat.Hashed, defaultUserPassword));
        }
        protected virtual void InstallActivityLogTypes()
        {
            var activityLogTypes = new List<ActivityLogType>
                                      {
                                          //admin area activities
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "AddNewUser",
                                                  Enabled = true,
                                                  Name = "Add a new user"
                                              },
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "AddNewUserRole",
                                                  Enabled = true,
                                                  Name = "Add a new user role"
                                              },
                                          new ActivityLogType
                                              {
                                                  SystemKeyword = "AddNewSetting",
                                                  Enabled = true,
                                                  Name = "Add a new setting"
                                              }
                                      };
            _activityLogTypeRepository.Insert(activityLogTypes);
        }


    }
}
