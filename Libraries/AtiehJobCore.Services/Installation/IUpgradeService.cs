namespace AtiehJobCore.Services.Installation
{
    public partial interface IUpgradeService
    {
        string DatabaseVersion();
        void UpgradeData(string fromVersion, string toVersion);
    }
}
