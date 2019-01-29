using AtiehJobCore.Common;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.MongoDb.Data;

namespace AtiehJobCore.Data.MongoDb
{
    public class MongoDbStartUpTask : IStartupTask
    {
        public void Execute()
        {
            var settings = EngineContext.Current.Resolve<DataSettings>();
            if (settings == null || !settings.IsValid())
            {
                return;
            }

            var provider = EngineContext.Current.Resolve<IDataProvider>();
            if (provider == null)
                throw new CustomException("No IDataProvider found");
        }

        //ensure that this task is run first
        public int Order => -1000;
    }
}
