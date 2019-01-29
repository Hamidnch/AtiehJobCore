using MongoDB.Bson;

namespace AtiehJobCore.Common.MongoDb.Data
{
    public abstract class ParentEntity
    {
        protected ParentEntity()
        {
            _id = ObjectId.GenerateNewId().ToString();
        }

        public string Id
        {
            get => _id;
            set => _id = string.IsNullOrEmpty(value) ? ObjectId.GenerateNewId().ToString() : value;
        }

        private string _id;

    }
}
