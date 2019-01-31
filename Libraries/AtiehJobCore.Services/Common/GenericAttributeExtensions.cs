using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.MongoDb;
using System;

namespace AtiehJobCore.Services.Common
{
    public static class GenericAttributeExtensions
    {
        /// <summary>
        /// Get an attribute of an entity
        /// </summary>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="key">Key</param>
        /// <returns>Attribute</returns>
        public static TPropType GetAttribute<TPropType>(this BaseMongoEntity entity, string key)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var genericAttributeService = EngineContext.Current.Resolve<IGenericAttributeService>();
            return genericAttributeService.GetAttributesForEntity<TPropType>(entity, key);


        }

    }
}
