using System;
using System.Collections.Generic;
using System.Text;

namespace AtiehJobCore.Web.Framework.Models
{
    public partial class UpgradeModel
    {
        public string DatabaseVersion { get; set; }
        public string ApplicationVersion { get; set; }
    }
}
