namespace AtiehJobCore.Services.MongoDb.Installation
{
    public partial interface IUpgradeService
    {
        string DatabaseVersion();
        void UpgradeData(string fromVersion, string toVersion);
    }
}
