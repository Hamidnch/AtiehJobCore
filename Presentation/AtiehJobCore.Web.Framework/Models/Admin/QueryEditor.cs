using AtiehJobCore.Web.Framework.Mvc.ModelBinding;

namespace AtiehJobCore.Web.Framework.Models.Admin
{
    public partial class QueryEditor
    {
        [AtiehJobResourceDisplayName("Admin.System.Field.QueryEditor")]
        public string Query { get; set; }
    }
}
