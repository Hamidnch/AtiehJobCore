using AtiehJobCore.Web.Framework.Mvc.ModelBinding;
using AtiehJobCore.Web.Framework.Validators.Admin;
using FluentValidation.Attributes;

namespace AtiehJobCore.Web.Framework.Models.Admin
{
    [Validator(typeof(AdminLanguageResourceValidator))]
    public partial class AdminLanguageResourceModel : BaseMongoEntityModel
    {
        [AtiehJobResourceDisplayName("Admin.Configuration.Languages.Resources.Fields.Name")]

        public string Name { get; set; }

        [AtiehJobResourceDisplayName("Admin.Configuration.Languages.Resources.Fields.Value")]

        public string Value { get; set; }

        public string LanguageId { get; set; }
    }
}
