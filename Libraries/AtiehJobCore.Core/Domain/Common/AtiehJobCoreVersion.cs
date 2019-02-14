using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Core.Domain.Common
{
    public class AtiehJobVersion : BaseMongoEntity
    {
        public string DataBaseVersion { get; set; }
    }
}
