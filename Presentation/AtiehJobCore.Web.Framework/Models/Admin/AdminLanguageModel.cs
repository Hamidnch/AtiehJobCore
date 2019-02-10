using AtiehJobCore.Web.Framework.Mvc.ModelBinding;
using AtiehJobCore.Web.Framework.Validators.Admin;
using FluentValidation.Attributes;
using System.Collections.Generic;

namespace AtiehJobCore.Web.Framework.Models.Admin
{
    [Validator(typeof(AdminLanguageValidator))]
    public partial class AdminLanguageModel : BaseMongoEntityModel
    {
        public AdminLanguageModel()
        {
            FlagFileNames = new List<string>();
            Search = new AdminLanguageResourceFilterModel();
        }
        [AtiehJobResourceDisplayName("Admin.Configuration.Languages.Fields.Name")]

        public string Name { get; set; }

        [AtiehJobResourceDisplayName("Admin.Configuration.Languages.Fields.LanguageCulture")]

        public string LanguageCulture { get; set; }

        [AtiehJobResourceDisplayName("Admin.Configuration.Languages.Fields.UniqueSeoCode")]

        public string UniqueSeoCode { get; set; }

        //flags
        [AtiehJobResourceDisplayName("Admin.Configuration.Languages.Fields.FlagImageFileName")]

        public string FlagImageFileName { get; set; }
        public IList<string> FlagFileNames { get; set; }

        [AtiehJobResourceDisplayName("Admin.Configuration.Languages.Fields.Rtl")]
        public bool Rtl { get; set; }

        [AtiehJobResourceDisplayName("Admin.Configuration.Languages.Fields.Published")]
        public bool Published { get; set; }

        [AtiehJobResourceDisplayName("Admin.Configuration.Languages.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        public AdminLanguageResourceFilterModel Search { get; set; }
    }
}
