namespace AtiehJobCore.Common.MongoDb.Domain.Users
{
    public partial class Role : BaseMongoEntity
    {
        public string Name { get; set; }

        public string UserId { get; set; }

        public bool Active { get; set; }

        public bool IsSystemRole { get; set; }

        public string SystemName { get; set; }

        public bool EnablePasswordLifetime { get; set; }

    }
}
