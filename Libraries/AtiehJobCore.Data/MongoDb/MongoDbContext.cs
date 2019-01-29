using System.Threading;
using System.Threading.Tasks;
using AtiehJobCore.Common.MongoDb.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Bindings;
using MongoDB.Driver.Core.Operations;

namespace AtiehJobCore.Data.MongoDb
{
    public class MongoDbContext : IMongoDbContext
    {
        protected IMongoDatabase MongoDatabase;
        protected IMongoClient MongoClient;

        public MongoDbContext()
        {

        }
        public MongoDbContext(string connectionString)
        {
            MongoClient = new MongoClient(connectionString);
            var databaseName = new MongoUrl(connectionString).DatabaseName;
            MongoDatabase = MongoClient.GetDatabase(databaseName);
        }

        public MongoDbContext(IMongoClient client)
        {
            var connectionString = DataSettingsHelper.ConnectionString();
            var databaseName = new MongoUrl(connectionString).DatabaseName;
            MongoDatabase = client.GetDatabase(databaseName);
            MongoClient = client;
        }

        public MongoDbContext(IMongoClient client, IMongoDatabase mongodatabase)
        {
            MongoDatabase = mongodatabase;
            MongoClient = client;
        }

        public IMongoClient Client()
        {
            return MongoClient;
        }

        public IMongoDatabase Database()
        {
            return MongoDatabase;
        }

        public TResult RunCommand<TResult>(string command)
        {
            return MongoDatabase.RunCommand<TResult>(command);
        }

        public TResult RunCommand<TResult>(string command, ReadPreference readpreference)
        {
            return MongoDatabase.RunCommand<TResult>(command, readpreference);
        }

        public BsonValue RunScript(string command, CancellationToken cancellationToken)
        {
            var script = new BsonJavaScript(command);
            var operation = new EvalOperation(MongoDatabase.DatabaseNamespace, script, null);
            var writeBinding = new WritableServerBinding(MongoClient.Cluster, NoCoreSession.NewHandle());
            return operation.Execute(writeBinding, CancellationToken.None);
        }

        public Task<BsonValue> RunScriptAsync(string command, CancellationToken cancellationToken)
        {
            var script = new BsonJavaScript(command);
            var operation = new EvalOperation(MongoDatabase.DatabaseNamespace, script, null);
            var writeBinding = new WritableServerBinding(MongoClient.Cluster, NoCoreSession.NewHandle());
            return operation.ExecuteAsync(writeBinding, CancellationToken.None);

        }
    }
}
