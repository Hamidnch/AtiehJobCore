using MongoDB.Bson;

namespace AtiehJobCore.Core.MongoDb
{
    public abstract class ParentMongoEntity
    {
        protected ParentMongoEntity()
        {
            _id = ObjectId.GenerateNewId().ToString();
        }

        public string Id
        {
            get => _id;
            set => _id = string.IsNullOrEmpty(value) ? ObjectId.GenerateNewId().ToString() : value;
        }

        private string _id;
        public string Description { get; set; }

    }
}
