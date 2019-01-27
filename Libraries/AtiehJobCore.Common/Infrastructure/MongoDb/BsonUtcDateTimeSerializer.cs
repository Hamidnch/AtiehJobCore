﻿using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace AtiehJobCore.Common.Infrastructure.MongoDb
{
    public class BsonUtcDateTimeSerializer : DateTimeSerializer
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value)
        {
            var utcValue = new DateTime(value.Ticks, DateTimeKind.Utc);
            base.Serialize(context, args, utcValue);
        }

        public override DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return new DateTime(base.Deserialize(context, args).Ticks, DateTimeKind.Unspecified);
        }
    }
}
