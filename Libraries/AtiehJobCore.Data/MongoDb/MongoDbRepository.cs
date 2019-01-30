using AtiehJobCore.Common.MongoDb;
using AtiehJobCore.Common.MongoDb.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AtiehJobCore.Data.MongoDb
{
    /// <inheritdoc />
    /// <summary>
    /// MongoDB repository
    /// </summary>
    public partial class MongoDbRepository<T> : IRepository<T> where T : BaseMongoEntity
    {
        #region Fields

        /// <summary>
        /// Gets the collection
        /// </summary>
        protected IMongoCollection<T> MongoCollection;
        public IMongoCollection<T> Collection => MongoCollection;

        /// <summary>
        /// Mongo Database
        /// </summary>
        protected IMongoDatabase MongoDatabase;
        public IMongoDatabase Database => MongoDatabase;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public MongoDbRepository()
        {
            var connectionString = DataSettingsHelper.ConnectionString();

            if (string.IsNullOrEmpty(connectionString))
            {
                return;
            }

            var client = new MongoClient(connectionString);
            var databaseName = new MongoUrl(connectionString).DatabaseName;
            MongoDatabase = client.GetDatabase(databaseName);
            MongoCollection = MongoDatabase.GetCollection<T>(typeof(T).Name);
        }
        public MongoDbRepository(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var databaseName = new MongoUrl(connectionString).DatabaseName;
            MongoDatabase = client.GetDatabase(databaseName);
            MongoCollection = MongoDatabase.GetCollection<T>(typeof(T).Name);
        }

        public MongoDbRepository(IMongoClient client)
        {
            var connectionString = DataSettingsHelper.ConnectionString();
            var databaseName = new MongoUrl(connectionString).DatabaseName;
            MongoDatabase = client.GetDatabase(databaseName);
            MongoCollection = MongoDatabase.GetCollection<T>(typeof(T).Name);
        }

        public MongoDbRepository(IMongoDatabase database)
        {
            MongoDatabase = database;
            MongoCollection = MongoDatabase.GetCollection<T>(typeof(T).Name);
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual T GetById(string id)
        {
            return MongoCollection.Find(e => e.Id == id).FirstOrDefault();
        }

        /// <inheritdoc />
        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual T Insert(T entity)
        {
            MongoCollection.InsertOne(entity);
            return entity;
        }

        /// <summary>
        /// Async Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<T> InsertAsync(T entity)
        {
            await MongoCollection.InsertOneAsync(entity);
            return entity;
        }


        /// <inheritdoc />
        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Insert(IEnumerable<T> entities)
        {
            MongoCollection.InsertMany(entities);
        }

        /// <inheritdoc />
        /// <summary>
        /// Async Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities)
        {
            var insertAsync = entities as T[] ?? entities.ToArray();
            await MongoCollection.InsertManyAsync(insertAsync);
            return insertAsync;
        }


        /// <inheritdoc />
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual T Update(T entity)
        {
            MongoCollection.ReplaceOne(x => x.Id == entity.Id,
                entity, new UpdateOptions() { IsUpsert = false });
            return entity;

        }

        /// <inheritdoc />
        /// <summary>
        /// Async Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<T> UpdateAsync(T entity)
        {
            await MongoCollection.ReplaceOneAsync(x => x.Id == entity.Id,
                entity, new UpdateOptions() { IsUpsert = false });
            return entity;
        }


        /// <inheritdoc />
        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Async Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities)
        {
            var updateAsync = entities as T[] ?? entities.ToArray();
            foreach (var entity in updateAsync)
            {
                await UpdateAsync(entity);
            }
            return updateAsync;
        }

        /// <inheritdoc />
        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(T entity)
        {
            MongoCollection.FindOneAndDelete(e => e.Id == entity.Id);
        }

        /// <inheritdoc />
        /// <summary>
        /// Async Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<T> DeleteAsync(T entity)
        {
            await MongoCollection.DeleteOneAsync(e => e.Id == entity.Id);
            return entity;
        }

        /// <inheritdoc />
        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                MongoCollection.FindOneAndDeleteAsync(e => e.Id == entity.Id);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Async Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual async Task<IEnumerable<T>> DeleteAsync(IEnumerable<T> entities)
        {
            var deleteAsync = entities as T[] ?? entities.ToArray();
            foreach (var entity in deleteAsync)
            {
                await DeleteAsync(entity);
            }
            return deleteAsync;
        }


        #endregion


        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Determines whether a list contains any elements
        /// </summary>
        /// <returns></returns>
        public virtual bool Any()
        {
            return MongoCollection.AsQueryable().Any();
        }

        /// <inheritdoc />
        /// <summary>
        /// Determines whether any element of a list satisfies a condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<T, bool>> where)
        {
            return MongoCollection.Find(where).Any();
        }

        /// <inheritdoc />
        /// <summary>
        /// Async determines whether a list contains any elements
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync()
        {
            return await MongoCollection.AsQueryable().AnyAsync();
        }

        /// <inheritdoc />
        /// <summary>
        /// Async determines whether any element of a list satisfies a condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return await MongoCollection.Find(where).AnyAsync();
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the number of elements in the specified sequence.
        /// </summary>
        /// <returns></returns>
        public virtual long Count()
        {
            return MongoCollection.CountDocuments(new BsonDocument());
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the number of elements in the specified sequence that satisfies a condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual long Count(Expression<Func<T, bool>> where)
        {
            return MongoCollection.CountDocuments(where);
        }

        /// <inheritdoc />
        /// <summary>
        /// Async returns the number of elements in the specified sequence
        /// </summary>
        /// <returns></returns>
        public virtual async Task<long> CountAsync()
        {
            return await MongoCollection.CountDocumentsAsync(new BsonDocument());
        }

        /// <inheritdoc />
        /// <summary>
        /// Async returns the number of elements in the specified sequence that satisfies a condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<long> CountAsync(Expression<Func<T, bool>> where)
        {
            return await MongoCollection.CountDocumentsAsync(where);
        }


        #endregion

        #region Properties

        /// <inheritdoc />
        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IMongoQueryable<T> Table => MongoCollection.AsQueryable();

        /// <inheritdoc />
        /// <summary>
        /// Get collection by filter definitions
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual IList<T> FindByFilterDefinition(FilterDefinition<T> query)
        {
            return MongoCollection.Find(query).ToList();
        }

        #endregion

    }
}
