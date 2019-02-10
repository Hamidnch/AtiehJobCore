using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Common
{
    public class AtiehJobCoreVersion : BaseMongoEntity
    {
        public string DataBaseVersion { get; set; }
    }
}
