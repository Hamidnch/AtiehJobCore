using System;
using System.Collections.Generic;
using AtiehJobCore.Web.Framework.Mvc.ModelBinding;

namespace AtiehJobCore.Web.Framework.Models.Admin
{
    public partial class SystemInfoModel : BaseMongoModel
    {
        public SystemInfoModel()
        {
            ServerVariables = new List<ServerVariableModel>();
            LoadedAssemblies = new List<LoadedAssembly>();
        }

        [AtiehJobResourceDisplayName("Admin.System.SystemInfo.ASPNETInfo")]
        public string AspNetInfo { get; set; }

        [AtiehJobResourceDisplayName("Admin.System.SystemInfo.MachineName")]
        public string MachineName { get; set; }


        [AtiehJobResourceDisplayName("Admin.System.SystemInfo.SiteVersion")]
        public string SiteVersion { get; set; }

        [AtiehJobResourceDisplayName("Admin.System.SystemInfo.OperatingSystem")]
        public string OperatingSystem { get; set; }

        [AtiehJobResourceDisplayName("Admin.System.SystemInfo.ServerLocalTime")]
        public DateTime ServerLocalTime { get; set; }

        [AtiehJobResourceDisplayName("Admin.System.SystemInfo.ServerTimeZone")]
        public string ServerTimeZone { get; set; }

        [AtiehJobResourceDisplayName("Admin.System.SystemInfo.UTCTime")]
        public DateTime UtcTime { get; set; }

        [AtiehJobResourceDisplayName("Admin.System.SystemInfo.Scheme")]
        public string RequestScheme { get; set; }

        [AtiehJobResourceDisplayName("Admin.System.SystemInfo.IsHttps")]
        public bool IsHttps { get; set; }

        [AtiehJobResourceDisplayName("Admin.System.SystemInfo.ServerVariables")]
        public IList<ServerVariableModel> ServerVariables { get; set; }

        [AtiehJobResourceDisplayName("Admin.System.SystemInfo.LoadedAssemblies")]
        public IList<LoadedAssembly> LoadedAssemblies { get; set; }

        public partial class ServerVariableModel : BaseMongoModel
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public partial class LoadedAssembly : BaseMongoModel
        {
            public string FullName { get; set; }
            public string Location { get; set; }
        }
    }

}
