using AtiehJobCore.Core.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace AtiehJobCore.Web.Framework.Security
{
    /// <summary>
    /// File permission helper
    /// </summary>
    public static class FilePermissionHelper
    {
        /// <summary>
        /// Check permissions
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="checkRead">Check read</param>
        /// <param name="checkWrite">Check write</param>
        /// <param name="checkModify">Check modify</param>
        /// <param name="checkDelete">Check delete</param>
        /// <returns>Result</returns>
        public static bool CheckPermissions(string path, bool checkRead, bool checkWrite,
            bool checkModify, bool checkDelete)
        {
            if (!Core.Infrastructure.OperatingSystem.IsWindows())
            {
                return true;
            }

            var flag = false;
            var flag2 = false;
            var flag3 = false;
            var flag4 = false;
            var flag5 = false;
            var flag6 = false;
            var flag7 = false;
            var flag8 = false;
            var current = WindowsIdentity.GetCurrent();
            AuthorizationRuleCollection rules;
            try
            {
                rules = new DirectorySecurity(path,
                    AccessControlSections.Access).GetAccessRules(true, true, typeof(SecurityIdentifier));
            }
            catch
            {
                return true;
            }
            try
            {
                foreach (FileSystemAccessRule rule in rules)
                {
                    if (!current.User.Equals(rule.IdentityReference))
                    {
                        continue;
                    }
                    switch (rule.AccessControlType)
                    {
                        case AccessControlType.Deny:
                            {
                                if ((FileSystemRights.Delete & rule.FileSystemRights) == FileSystemRights.Delete)
                                    flag4 = true;
                                if ((FileSystemRights.Modify & rule.FileSystemRights) == FileSystemRights.Modify)
                                    flag3 = true;

                                if ((FileSystemRights.Read & rule.FileSystemRights) == FileSystemRights.Read)
                                    flag = true;

                                if ((FileSystemRights.Write & rule.FileSystemRights) == FileSystemRights.Write)
                                    flag2 = true;

                                continue;
                            }
                        case AccessControlType.Allow:
                            {
                                if ((FileSystemRights.Delete & rule.FileSystemRights) == FileSystemRights.Delete)
                                {
                                    flag8 = true;
                                }
                                if ((FileSystemRights.Modify & rule.FileSystemRights) == FileSystemRights.Modify)
                                {
                                    flag7 = true;
                                }
                                if ((FileSystemRights.Read & rule.FileSystemRights) == FileSystemRights.Read)
                                {
                                    flag5 = true;
                                }
                                if ((FileSystemRights.Write & rule.FileSystemRights) == FileSystemRights.Write)
                                {
                                    flag6 = true;
                                }

                                break;
                            }
                    }
                }
                foreach (var reference in current.Groups)
                {
                    foreach (FileSystemAccessRule rule2 in rules)
                    {
                        if (!reference.Equals(rule2.IdentityReference))
                        {
                            continue;
                        }
                        switch (rule2.AccessControlType)
                        {
                            case AccessControlType.Deny:
                                {
                                    if ((FileSystemRights.Delete & rule2.FileSystemRights) == FileSystemRights.Delete)
                                        flag4 = true;
                                    if ((FileSystemRights.Modify & rule2.FileSystemRights) == FileSystemRights.Modify)
                                        flag3 = true;
                                    if ((FileSystemRights.Read & rule2.FileSystemRights) == FileSystemRights.Read)
                                        flag = true;
                                    if ((FileSystemRights.Write & rule2.FileSystemRights) == FileSystemRights.Write)
                                        flag2 = true;
                                    continue;
                                }
                            case AccessControlType.Allow:
                                {
                                    if ((FileSystemRights.Delete & rule2.FileSystemRights) == FileSystemRights.Delete)
                                        flag8 = true;
                                    if ((FileSystemRights.Modify & rule2.FileSystemRights) == FileSystemRights.Modify)
                                        flag7 = true;
                                    if ((FileSystemRights.Read & rule2.FileSystemRights) == FileSystemRights.Read)
                                        flag5 = true;
                                    if ((FileSystemRights.Write & rule2.FileSystemRights) == FileSystemRights.Write)
                                        flag6 = true;
                                    break;
                                }
                        }
                    }
                }
                var flag9 = !flag4 && flag8;
                var flag10 = !flag3 && flag7;
                var flag11 = !flag && flag5;
                var flag12 = !flag2 && flag6;
                var flag13 = true;
                if (checkRead)
                {
                    flag13 = flag13 && flag11;
                }
                if (checkWrite)
                {
                    flag13 = flag13 && flag12;
                }
                if (checkModify)
                {
                    flag13 = flag13 && flag10;
                }
                if (checkDelete)
                {
                    flag13 = flag13 && flag9;
                }
                return flag13;
            }
            catch (IOException)
            {
            }
            return false;
        }

        /// <summary>
        /// Gets a list of directories (physical paths) which require write permission
        /// </summary>
        /// <returns>Result</returns>
        public static IEnumerable<string> GetDirectoriesWrite()
        {
            var rootDir = CommonHelper.MapPath("~/");
            var dirsToCheck = new List<string>
            {
                Path.Combine(rootDir, "App_Data"),
                Path.Combine(rootDir, "bin"),
                Path.Combine(rootDir, "logs"),
                Path.Combine(rootDir, "wwwroot\\css"),
                Path.Combine(rootDir, "wwwroot\\js"),
                Path.Combine(rootDir, "wwwroot\\UploadedFiles"),
                Path.Combine(rootDir, "wwwroot\\webfonts"),
                Path.Combine(rootDir, "wwwroot\\img"),
                Path.Combine(rootDir, "wwwroot\\content\\files\\exportimport"),
                Path.Combine(rootDir, "plugins"),
                Path.Combine(rootDir, "plugins\\bin")
            };
            return dirsToCheck;
        }

        /// <summary>
        /// Gets a list of files (physical paths) which require write permission
        /// </summary>
        /// <returns>Result</returns>
        public static IEnumerable<string> GetFilesWrite()
        {
            var rootDir = CommonHelper.MapPath("~/");
            var filesToCheck = new List<string>
            {
                Path.Combine(rootDir, "web.config")
            };
            if (Core.Infrastructure.OperatingSystem.IsWindows())
            {
                filesToCheck.Add(Path.Combine(rootDir, "App_Data\\InstalledPlugins.txt"));
                filesToCheck.Add(Path.Combine(rootDir, "App_Data\\Settings.txt"));
            }
            else
            {
                filesToCheck.Add(Path.Combine(rootDir, "App_Data/InstalledPlugins.txt"));
                filesToCheck.Add(Path.Combine(rootDir, "App_Data/Settings.txt"));
            }
            return filesToCheck;
        }
    }
}
