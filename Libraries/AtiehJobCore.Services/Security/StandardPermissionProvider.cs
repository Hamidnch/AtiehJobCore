using AtiehJobCore.Core.Domain.Security;
using AtiehJobCore.Core.Domain.Users;
using System.Collections.Generic;

namespace AtiehJobCore.Services.Security
{
    /// <inheritdoc />
    /// <summary>
    /// Standard permission provider
    /// </summary>
    public partial class StandardPermissionProvider : IPermissionProvider
    {
        //admin area permissions
        public static readonly PermissionRecord AccessAdminPanel =
            new PermissionRecord
            {
                Name = "Access admin area",
                SystemName = PermissionSystemName.AccessAdminPanel,
                Category = "Standard"
            };
        public static readonly PermissionRecord ManageUsers = new PermissionRecord
        {
            Name = "Admin area. Manage Users",
            SystemName = PermissionSystemName.Users,
            Category = "Users"
        };


        public static readonly PermissionRecord ManageNews = new PermissionRecord
        {
            Name = "Admin area. Manage News",
            SystemName = PermissionSystemName.News,
            Category = "Content Management"
        };
        public static readonly PermissionRecord ManageCountries = new PermissionRecord
        {
            Name = "Admin area. Manage Countries",
            SystemName = PermissionSystemName.Countries,
            Category = "Configuration"
        };
        public static readonly PermissionRecord ManageLanguages = new PermissionRecord
        {
            Name = "Admin area. Manage Languages",
            SystemName = PermissionSystemName.Languages,
            Category = "Configuration"
        };
        public static readonly PermissionRecord ManageSettings = new PermissionRecord
        {
            Name = "Admin area. Manage Settings",
            SystemName = PermissionSystemName.Settings,
            Category = "Configuration"
        };
        public static readonly PermissionRecord ManageActivityLog = new PermissionRecord
        {
            Name = "Admin area. Manage Activity Log",
            SystemName = PermissionSystemName.ActivityLog,
            Category = "Configuration"
        };
        public static readonly PermissionRecord ManageAcl = new PermissionRecord
        {
            Name = "Admin area. Manage ACL",
            SystemName = PermissionSystemName.Acl,
            Category = "Configuration"
        };
        public static readonly PermissionRecord ManageEmailAccounts = new PermissionRecord
        {
            Name = "Admin area. Manage Email Accounts",
            SystemName = PermissionSystemName.EmailAccounts,
            Category = "Configuration"
        };
        public static readonly PermissionRecord ManageSystemLog = new PermissionRecord
        {
            Name = "Admin area. Manage System Log",
            SystemName = PermissionSystemName.SystemLog,
            Category = "Configuration"
        };
        public static readonly PermissionRecord ManageMessageContactForm = new PermissionRecord
        {
            Name = "Admin area. Manage Message Contact form",
            SystemName = PermissionSystemName.MessageContactForm,
            Category = "Configuration"
        };

        public static readonly PermissionRecord SiteAllowNavigation = new PermissionRecord
        {
            Name = "Atieh job. Allow navigation",
            SystemName = PermissionSystemName.SiteAllowNavigation,
            Category = "Site"
        };

        public static readonly PermissionRecord AccessClosedSite = new PermissionRecord
        {
            Name = "Atieh job. Access a closed store",
            SystemName = PermissionSystemName.AccessClosedSite,
            Category = "Site"
        };

        public virtual IEnumerable<PermissionRecord> GetPermissions()
        {
            return new[]
            {
                AccessAdminPanel,
                ManageUsers,
                ManageNews,
                ManageCountries,
                ManageLanguages,
                ManageSettings,
                ManageActivityLog,
                ManageAcl,
                ManageEmailAccounts,
                ManageSystemLog,
                ManageMessageContactForm,
                SiteAllowNavigation,
                AccessClosedSite
            };
        }

        public virtual IEnumerable<DefaultPermissionRecord> GetDefaultPermissions()
        {
            return new[]
            {
                new DefaultPermissionRecord
                {
                    UserRoleSystemName = SystemUserRoleNames.Administrators,
                    PermissionRecords = new[]
                    {
                        AccessAdminPanel,
                        ManageUsers,
                        ManageNews,
                        ManageCountries,
                        ManageLanguages,
                        ManageSettings,
                        ManageActivityLog,
                        ManageAcl,
                        ManageEmailAccounts,
                        ManageSystemLog,
                        ManageMessageContactForm,
                        SiteAllowNavigation,
                        AccessClosedSite
                    }
                },
                new DefaultPermissionRecord
                {
                    UserRoleSystemName = SystemUserRoleNames.Guests,
                    PermissionRecords = new[]
                    {
                        SiteAllowNavigation
                    }
                },
                new DefaultPermissionRecord
                {
                    UserRoleSystemName = SystemUserRoleNames.Registered,
                    PermissionRecords = new[]
                    {
                        SiteAllowNavigation
                    }
                }
            };
        }
    }
}
