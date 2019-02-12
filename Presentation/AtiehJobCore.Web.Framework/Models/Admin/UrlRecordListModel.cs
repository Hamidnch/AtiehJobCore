using AtiehJobCore.Web.Framework.Mvc.ModelBinding;

namespace AtiehJobCore.Web.Framework.Models.Admin
{
    public partial class UrlRecordListModel : BaseMongoModel
    {
        [AtiehJobResourceDisplayName("Admin.System.SeNames.Name")]

        public string SeName { get; set; }
    }
}
