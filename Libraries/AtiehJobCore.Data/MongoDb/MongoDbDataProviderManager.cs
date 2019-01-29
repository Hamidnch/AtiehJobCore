using AtiehJobCore.Common;
using AtiehJobCore.Common.MongoDb.Data;

namespace AtiehJobCore.Data.MongoDb
{
    public partial class MongoDbDataProviderManager : BaseDataProviderManager
    {
        public MongoDbDataProviderManager(DataSettings settings) : base(settings)
        {
        }

        public override IDataProvider LoadDataProvider()
        {
            var providerName = Settings.DataProvider;
            if (!string.IsNullOrWhiteSpace(providerName) && providerName.ToLowerInvariant() != "mongodb")
                throw new CustomException($"Not supported dataprovider name: {providerName}");

            return new MongoDbDataProvider();
        }
    }
}
