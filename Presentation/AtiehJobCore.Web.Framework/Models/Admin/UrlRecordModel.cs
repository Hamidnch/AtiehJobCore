using AtiehJobCore.Web.Framework.Mvc.ModelBinding;

namespace AtiehJobCore.Web.Framework.Models.Admin
{
    public partial class UrlRecordModel : BaseMongoEntityModel
    {
        [AtiehJobResourceDisplayName("Admin.System.SeNames.Name")]

        public string Name { get; set; }

        [AtiehJobResourceDisplayName("Admin.System.SeNames.EntityId")]
        public string EntityId { get; set; }

        [AtiehJobResourceDisplayName("Admin.System.SeNames.EntityName")]
        public string EntityName { get; set; }

        [AtiehJobResourceDisplayName("Admin.System.SeNames.IsActive")]
        public bool IsActive { get; set; }

        [AtiehJobResourceDisplayName("Admin.System.SeNames.Language")]
        public string Language { get; set; }

        [AtiehJobResourceDisplayName("Admin.System.SeNames.Details")]
        public string DetailsUrl { get; set; }
    }
}
