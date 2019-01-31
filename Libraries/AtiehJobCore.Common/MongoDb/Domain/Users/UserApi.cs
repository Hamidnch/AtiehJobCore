namespace AtiehJobCore.Common.MongoDb.Domain.Users
{
    public partial class UserApi : BaseMongoEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PrivateKey { get; set; }
        public bool IsActive { get; set; }
    }
}
