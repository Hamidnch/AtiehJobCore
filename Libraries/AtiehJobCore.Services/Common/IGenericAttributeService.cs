using AtiehJobCore.Common.MongoDb;

namespace AtiehJobCore.Services.Common
{
    /// <summary>
    /// Generic attribute service interface
    /// </summary>
    public partial interface IGenericAttributeService
    {
        void SaveAttribute<TPropType>(BaseMongoEntity entity, string key, TPropType value);
        TPropType GetAttributesForEntity<TPropType>(BaseMongoEntity entity, string key);
    }
}
