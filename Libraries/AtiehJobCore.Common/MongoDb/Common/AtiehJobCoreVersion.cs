using AtiehJobCore.Common.MongoDb.Data;

namespace AtiehJobCore.Common.MongoDb.Common
{
    public class AtiehJobCoreVersion : BaseMongoEntity
    {
        public string DataBaseVersion { get; set; }
    }
}
