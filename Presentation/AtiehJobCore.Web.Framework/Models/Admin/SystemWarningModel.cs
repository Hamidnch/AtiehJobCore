namespace AtiehJobCore.Web.Framework.Models.Admin
{
    public partial class SystemWarningModel : BaseMongoModel
    {
        public SystemWarningLevel Level { get; set; }

        public string Text { get; set; }
    }

    public enum SystemWarningLevel
    {
        Pass,
        Warning,
        Fail
    }
}
