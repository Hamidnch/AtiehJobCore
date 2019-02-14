﻿using AtiehJobCore.Core.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace AtiehJobCore.Core.Caching
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a manager for caching in Redis store (http://redis.io/).
    /// Mostly it'll be used when running in a web farm or Azure. But of course it can be also used on any server or environment
    /// </summary>
    public partial class RedisCacheManager : ICacheManager
    {
        #region Fields

        private readonly IRedisConnectionWrapper _connectionWrapper;
        private readonly IDatabase _db;

        #endregion

        #region Ctor

        public RedisCacheManager(/*ICacheManager perRequestCacheManager,*/
            IRedisConnectionWrapper connectionWrapper,
            AtiehJobConfig config)
        {
            if (string.IsNullOrEmpty(config.RedisCachingConnectionString))
                throw new Exception("Redis connection string is empty");

            this._connectionWrapper = connectionWrapper;

            this._db = _connectionWrapper.GetDatabase();
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Key of cached item</param>
        /// <returns>The cached value associated with the specified key</returns>
        public virtual T Get<T>(string key)
        {

            //get serialized item from cache
            var serializedItem = _db.StringGet(key);
            if (!serializedItem.HasValue)
                return default(T);

            //deserialize item
            var item = JsonConvert.DeserializeObject<T>(serializedItem);
            if (item == null)
                return default(T);

            return item;
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds the specified key and object to the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <param name="data">Value for caching</param>
        /// <param name="cacheTime">Cache time in minutes</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            //set cache time
            var expiresIn = TimeSpan.FromMinutes(cacheTime);

            //serialize item
            var serializedItem = JsonConvert.SerializeObject(data);

            //and set it to cache
            _db.StringSet(key, serializedItem, expiresIn);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>True if item already is in cache; otherwise false</returns>
        public virtual bool IsSet(string key)
        {
            return _db.KeyExists(key);
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        public virtual void Remove(string key)
        {
            //remove item from caches
            _db.KeyDelete(key);
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes items by key pattern
        /// </summary>
        /// <param name="pattern">String key pattern</param>
        public virtual void RemoveByPattern(string pattern)
        {
            foreach (var endPoint in _connectionWrapper.GetEndPoints())
            {
                var server = _connectionWrapper.GetServer(endPoint);
                var keys = server.Keys(database: _db.Database, pattern: $"*{pattern}*");
                foreach (var key in keys)
                    Remove(key);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear()
        {
            foreach (var endPoint in _connectionWrapper.GetEndPoints())
            {
                var server = _connectionWrapper.GetServer(endPoint);
                //that's why we simply interate through all elements now
                var keys = server.Keys(database: _db.Database);
                foreach (var key in keys)
                    Remove(key);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Dispose cache manager
        /// </summary>
        public virtual void Dispose()
        {
        }

        #endregion
    }
}
