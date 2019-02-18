using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Extensions;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Events;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtiehJobCore.Services.Users
{
    /// <inheritdoc />
    /// <summary>
    /// User attribute service
    /// </summary>
    public partial class UserAttributeService : IUserAttributeService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        private const string UserAttributesAllKey = "AtiehJob.userattribute.all";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : user attribute ID
        /// </remarks>
        private const string UserAttributesByIdKey = "AtiehJob.userattribute.id-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : user attribute ID
        /// </remarks>
        private const string UserAttributeValuesAllKey = "AtiehJob.userattributevalue.all-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : user attribute value ID
        /// </remarks>
        private const string UserAttributeValuesByIdKey = "AtiehJob.userattributevalue.id-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string UserAttributesPatternKey = "AtiehJob.userattribute.";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string UserAttributeValuesPatternKey = "AtiehJob.userattributevalue.";
        #endregion

        #region Fields

        private readonly IRepository<UserAttribute> _userAttributeRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="userAttributeRepository">User attribute repository</param>
        /// <param name="eventPublisher">Event published</param>
        public UserAttributeService(ICacheManager cacheManager,
            IRepository<UserAttribute> userAttributeRepository,
            IEventPublisher eventPublisher)
        {
            this._cacheManager = cacheManager;
            this._userAttributeRepository = userAttributeRepository;
            this._eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Deletes a user attribute
        /// </summary>
        /// <param name="userAttribute">User attribute</param>
        public virtual void DeleteUserAttribute(UserAttribute userAttribute)
        {
            if (userAttribute == null)
                throw new ArgumentNullException(nameof(userAttribute));

            _userAttributeRepository.Delete(userAttribute);

            _cacheManager.RemoveByPattern(UserAttributesPatternKey);
            _cacheManager.RemoveByPattern(UserAttributeValuesPatternKey);

            //event notification
            _eventPublisher.EntityDeleted(userAttribute);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets all user attributes
        /// </summary>
        /// <returns>User attributes</returns>
        public virtual IList<UserAttribute> GetAllUserAttributes()
        {
            const string key = UserAttributesAllKey;
            return _cacheManager.Get(key, () =>
            {
                var query = from ca in _userAttributeRepository.Table
                            orderby ca.DisplayOrder
                            select ca;
                return query.ToList();
            });
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a user attribute 
        /// </summary>
        /// <param name="userAttributeId">User attribute identifier</param>
        /// <returns>User attribute</returns>
        public virtual UserAttribute GetUserAttributeById(string userAttributeId)
        {
            var key = string.Format(UserAttributesByIdKey, userAttributeId);
            return _cacheManager.Get(key, () => _userAttributeRepository.GetById(userAttributeId));
        }

        /// <inheritdoc />
        /// <summary>
        /// Inserts a user attribute
        /// </summary>
        /// <param name="userAttribute">User attribute</param>
        public virtual void InsertUserAttribute(UserAttribute userAttribute)
        {
            if (userAttribute == null)
                throw new ArgumentNullException(nameof(userAttribute));

            _userAttributeRepository.Insert(userAttribute);

            _cacheManager.RemoveByPattern(UserAttributesPatternKey);
            _cacheManager.RemoveByPattern(UserAttributeValuesPatternKey);

            //event notification
            _eventPublisher.EntityInserted(userAttribute);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the user attribute
        /// </summary>
        /// <param name="userAttribute">User attribute</param>
        public virtual void UpdateUserAttribute(UserAttribute userAttribute)
        {
            if (userAttribute == null)
                throw new ArgumentNullException(nameof(userAttribute));

            _userAttributeRepository.Update(userAttribute);

            _cacheManager.RemoveByPattern(UserAttributesPatternKey);
            _cacheManager.RemoveByPattern(UserAttributeValuesPatternKey);

            //event notification
            _eventPublisher.EntityUpdated(userAttribute);
        }

        /// <inheritdoc />
        /// <summary>
        /// Deletes a user attribute value
        /// </summary>
        /// <param name="userAttributeValue">User attribute value</param>
        public virtual void DeleteUserAttributeValue(UserAttributeValue userAttributeValue)
        {
            if (userAttributeValue == null)
                throw new ArgumentNullException(nameof(userAttributeValue));

            var updateBuilder = MongoDB.Driver.Builders<UserAttribute>.Update;
            var update = updateBuilder.Pull(p => p.UserAttributeValues, userAttributeValue);
            _userAttributeRepository.Collection.UpdateOneAsync(new BsonDocument("_id", userAttributeValue.UserAttributeId), update);

            _cacheManager.RemoveByPattern(UserAttributesPatternKey);
            _cacheManager.RemoveByPattern(UserAttributeValuesPatternKey);

            //event notification
            _eventPublisher.EntityDeleted(userAttributeValue);
        }

        /// <inheritdoc />
        /// <summary>
        /// Inserts a user attribute value
        /// </summary>
        /// <param name="userAttributeValue">User attribute value</param>
        public virtual void InsertUserAttributeValue(UserAttributeValue userAttributeValue)
        {
            if (userAttributeValue == null)
                throw new ArgumentNullException(nameof(userAttributeValue));

            var updateBuilder = Builders<UserAttribute>.Update;
            var update = updateBuilder.AddToSet(p => p.UserAttributeValues, userAttributeValue);
            _userAttributeRepository.Collection.UpdateOneAsync(new BsonDocument("_id", userAttributeValue.UserAttributeId), update);

            _cacheManager.RemoveByPattern(UserAttributesPatternKey);
            _cacheManager.RemoveByPattern(UserAttributeValuesPatternKey);

            //event notification
            _eventPublisher.EntityInserted(userAttributeValue);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the user attribute value
        /// </summary>
        /// <param name="userAttributeValue">User attribute value</param>
        public virtual void UpdateUserAttributeValue(UserAttributeValue userAttributeValue)
        {
            if (userAttributeValue == null)
                throw new ArgumentNullException(nameof(userAttributeValue));

            var builder = Builders<UserAttribute>.Filter;
            var filter = builder.Eq(x => x.Id, userAttributeValue.UserAttributeId);
            filter = filter & builder.ElemMatch(x => x.UserAttributeValues, y => y.Id == userAttributeValue.Id);
            var update = Builders<UserAttribute>.Update
                .Set(x => x.UserAttributeValues.ElementAt(-1).DisplayOrder, userAttributeValue.DisplayOrder)
                .Set(x => x.UserAttributeValues.ElementAt(-1).IsPreSelected, userAttributeValue.IsPreSelected)
                .Set(x => x.UserAttributeValues.ElementAt(-1).Locales, userAttributeValue.Locales)
                .Set(x => x.UserAttributeValues.ElementAt(-1).Name, userAttributeValue.Name);

            var result = _userAttributeRepository.Collection.UpdateManyAsync(filter, update).Result;

            _cacheManager.RemoveByPattern(UserAttributesPatternKey);
            _cacheManager.RemoveByPattern(UserAttributeValuesPatternKey);

            //event notification
            _eventPublisher.EntityUpdated(userAttributeValue);
        }

        #endregion
    }
}
