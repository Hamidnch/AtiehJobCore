using AtiehJobCore.Core.Domain.Common;
using AtiehJobCore.Core.MongoDb;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Core.Utilities;
using AtiehJobCore.Services.Events;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace AtiehJobCore.Services.Common
{
    /// <inheritdoc />
    /// <summary>
    /// Generic attribute service
    /// </summary>
    public partial class GenericAttributeService : IGenericAttributeService
    {

        #region Fields

        private readonly IRepository<BaseMongoEntity> _baseMongoEntityRepository;
        private readonly IRepository<GenericAttributeBaseEntity> _genericAttributeBaseEntityRepository;
        private readonly IEventPublisher _eventPublisher;
        #endregion

        #region Ctor

        public GenericAttributeService(
            IRepository<BaseMongoEntity> baseMongoEntityRepository,
            IRepository<GenericAttributeBaseEntity> genericAttributeBaseEntityRepository,
            IEventPublisher eventPublisher)
        {
            this._baseMongoEntityRepository = baseMongoEntityRepository;
            this._genericAttributeBaseEntityRepository = genericAttributeBaseEntityRepository;
            this._eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Save attribute value
        /// </summary>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public virtual void SaveAttribute<TPropType>(BaseMongoEntity entity, string key, TPropType value)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var keyGroup = entity.GetType().Name;

            var collection = _baseMongoEntityRepository.Database.GetCollection<GenericAttributeBaseEntity>(keyGroup);
            var query = _baseMongoEntityRepository.Database.GetCollection<GenericAttributeBaseEntity>(keyGroup)
                .Find(new BsonDocument("_id", entity.Id)).FirstOrDefault();

            var props = query.GenericAttributes;

            var prop = props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.OrdinalIgnoreCase)); //should be culture invariant

            var valueStr = CommonHelper.To<string>(value);

            if (prop != null)
            {
                if (string.IsNullOrWhiteSpace(valueStr))
                {
                    //delete
                    var builder = Builders<GenericAttributeBaseEntity>.Update;
                    var updateFilter = builder.PullFilter(x => x.GenericAttributes, y => y.Key == prop.Key);
                    var result = collection.UpdateManyAsync(new BsonDocument("_id", entity.Id), updateFilter).Result;
                }
                else
                {
                    //update
                    prop.Value = valueStr;
                    var builder = Builders<GenericAttributeBaseEntity>.Filter;
                    var filter = builder.Eq(x => x.Id, entity.Id);
                    filter = filter & builder.Where(x => x.GenericAttributes.Any(y => y.Key == prop.Key));
                    var update = Builders<GenericAttributeBaseEntity>.Update
                        .Set(x => x.GenericAttributes.ElementAt(-1).Value, prop.Value);

                    var result = collection.UpdateManyAsync(filter, update).Result;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(valueStr))
                {
                    return;
                }

                prop = new GenericAttribute
                {
                    Key = key,
                    Value = valueStr
                };
                var updateBuilder = Builders<GenericAttributeBaseEntity>.Update;
                var update = updateBuilder.AddToSet(p => p.GenericAttributes, prop);
                var result = collection.UpdateOneAsync(new BsonDocument("_id", entity.Id), update).Result;
            }
        }

        public virtual TPropType GetAttributesForEntity<TPropType>(BaseMongoEntity entity, string key)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var collection = _genericAttributeBaseEntityRepository.Database.GetCollection<GenericAttributeBaseEntity>(entity.GetType().Name)
                .Find(new BsonDocument("_id", entity.Id)).FirstOrDefault();

            var props = collection.GenericAttributes;
            if (props == null)
                return default(TPropType);
            props = props.ToList();
            if (!props.Any())
                return default(TPropType);

            var prop = props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

            if (prop == null || string.IsNullOrEmpty(prop.Value))
                return default(TPropType);

            return CommonHelper.To<TPropType>(prop.Value);
        }

        #endregion
    }
}
