namespace AtiehJobCore.Web.Framework.Models
{
    public class DeleteConfirmationModel : BaseMongoEntityModel
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string WindowId { get; set; }
    }
}
