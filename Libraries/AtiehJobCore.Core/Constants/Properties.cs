namespace AtiehJobCore.Core.Constants
{
    public static class Properties
    {
        public static readonly string CreatedByBrowserName = nameof(CreatedByBrowserName);
        public static readonly string ModifiedByBrowserName = nameof(ModifiedByBrowserName);
        public static readonly string CreatedByIp = nameof(CreatedByIp);
        public static readonly string ModifiedByIp = nameof(ModifiedByIp);
        public static readonly string CreatedByUserId = nameof(CreatedByUserId);
        public static readonly string ModifiedByUserId = nameof(ModifiedByUserId);
        public static readonly string CreatedDateTime = nameof(CreatedDateTime);
        public static readonly string ModifiedDateTime = nameof(ModifiedDateTime);

        public const string AntiForgeryToken = "__RequestVerificationToken";
    }
}
