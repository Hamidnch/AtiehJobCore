using System;
using AtiehJobCore.Core.Configuration;
using AtiehJobCore.Core.Domain.Logging;
using AtiehJobCore.Core.Domain.Users;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace AtiehJobCore.Core.Infrastructure.AutoMapper
{
    public class AtiehJobMapperConfiguration
    {

        /// <summary>
        /// Register MongoDB mappings
        /// </summary>
        /// <param name="config">Config</param>
        public static void RegisterMongoDbMappings(AtiehJobConfig config)
        {
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>
                (new DecimalSerializer(BsonType.Decimal128)));
            BsonSerializer.RegisterSerializer(typeof(DateTime), new BsonUtcDateTimeSerializer());

            //global set an equivalent of [BsonIgnoreExtraElements] for every Domain Model
            var cp = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true)
            };
            ConventionRegistry.Register("ApplicationConventions", cp, t => true);


            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.PasswordFormat);
            });



            BsonClassMap.RegisterClassMap<UserHistoryPassword>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.PasswordFormat);
            });


            BsonClassMap.RegisterClassMap<Role>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.UserId);
            });


            BsonClassMap.RegisterClassMap<Log>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.LogLevel);
            });
        }
    }
}
