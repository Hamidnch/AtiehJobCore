using AtiehJobCore.Core;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Core.MongoDb.Data;

namespace AtiehJobCore.Data.StartUpTask
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
                throw new AtiehJobException("No IDataProvider found");
        }

        //ensure that this task is run first
        public int Order => -1000;
    }
}
