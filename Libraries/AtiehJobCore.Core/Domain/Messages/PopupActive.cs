﻿using AtiehJobCore.Core.MongoDb;
using System;

namespace AtiehJobCore.Core.Domain.Messages
{
    public partial class PopupActive : BaseMongoEntity
    {
        public string Name { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
        public string UserActionId { get; set; }
        public int PopupTypeId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}