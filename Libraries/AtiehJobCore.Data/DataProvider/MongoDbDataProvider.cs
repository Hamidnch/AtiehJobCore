using AtiehJobCore.Core.MongoDb.Data;

namespace AtiehJobCore.Data.DataProvider
{
    public class MongoDbDataProvider : IDataProvider
    {
        #region Methods


        /// <inheritdoc />
        /// <summary>
        /// Initialize database
        /// </summary>
        public virtual void InitDatabase()
        {
            DataSettingsHelper.InitConnectionString();
        }

        #endregion
    }
}
