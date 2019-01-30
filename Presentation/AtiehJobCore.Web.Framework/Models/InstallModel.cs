using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AtiehJobCore.Web.Framework.Validators.Install;
using FluentValidation.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AtiehJobCore.Web.Framework.Models
{
    [Validator(typeof(InstallValidator))]
    public partial class InstallModel : BaseMongoModel
    {
        public InstallModel()
        {
            this.AvailableLanguages = new List<SelectListItem>();
            this.AvailableCollation = new List<SelectListItem>();
        }
        public string AdminEmail { get; set; }
        [DataType(DataType.Password)]
        public string AdminPassword { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string DatabaseConnectionString { get; set; }
        public string DataProvider { get; set; }
        public bool MongoDbConnectionInfo { get; set; }
        public string MongoDbServerName { get; set; }
        public string MongoDbDatabaseName { get; set; }
        public string MongoDbUsername { get; set; }
        [DataType(DataType.Password)]
        public string MongoDbPassword { get; set; }
        public bool DisableSampleDataOption { get; set; }
        public bool InstallSampleData { get; set; }
        public bool Installed { get; set; }
        public string Collation { get; set; }
        public List<SelectListItem> AvailableLanguages { get; set; }
        public List<SelectListItem> AvailableCollation { get; set; }
    }
}
