using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AtiehJobCore.Core.Caching
{
    public partial class DistributedRedisCacheExtended : IDistributedRedisCacheExtended, IDisposable
    {
        private readonly RedisCacheOptions _options;
        private readonly ConnectionMultiplexer _connection;
        private readonly IDatabase _db;

        private bool _isDisposed;


        public DistributedRedisCacheExtended(IOptions<RedisCacheOptions> redisCacheOptions)
        {
            this._options = redisCacheOptions.Value;
            this._connection = ConnectionMultiplexer.Connect(GetConnectionOptions());
            this._db = _connection.GetDatabase();
        }

        private ConfigurationOptions GetConnectionOptions()
        {
            var redisConnectionOptions = (_options.ConfigurationOptions != null)
                ? ConfigurationOptions.Parse(_options.ConfigurationOptions.ToString())
                : ConfigurationOptions.Parse(_options.Configuration);

            redisConnectionOptions.AbortOnConnectFail = false;

            return redisConnectionOptions;
        }

        public async Task ClearAsync()
        {
            foreach (var endPoint in _connection.GetEndPoints())
            {
                var server = _connection.GetServer(endPoint);
                var keys = server.Keys(_db.Database).ToArray();
                await _db.KeyDeleteAsync(keys);
            }
        }

        public async Task RemoveByPatternAsync(string pattern)
        {
            foreach (var endPoint in _connection.GetEndPoints())
            {
                var server = _connection.GetServer(endPoint);
                var keys = server.Keys(_db.Database, $"*{pattern}*");
                await _db.KeyDeleteAsync(keys.ToArray());
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }



        private void Dispose(bool disposing)
        {
            if (this._isDisposed)
            {
                return;
            }

            if (disposing)
            {
                _connection?.Close();
            }

            this._isDisposed = true;
        }

    }
}
