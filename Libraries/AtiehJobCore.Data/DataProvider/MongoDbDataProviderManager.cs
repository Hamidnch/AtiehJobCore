using AtiehJobCore.Core;
using AtiehJobCore.Core.MongoDb.Data;

namespace AtiehJobCore.Data.DataProvider
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
                throw new AtiehJobException($"Not supported dataprovider name: {providerName}");

            return new MongoDbDataProvider();
        }
    }
}
