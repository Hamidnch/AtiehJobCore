using AtiehJobCore.Common.MongoDb.Data;

namespace AtiehJobCore.Data.MongoDb
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
