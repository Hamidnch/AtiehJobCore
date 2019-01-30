using AtiehJobCore.Common.MongoDb.Domain.Common;
using System.Collections.Generic;

namespace AtiehJobCore.Common.MongoDb
{
    public abstract partial class BaseMongoEntity : ParentMongoEntity
    {
        protected BaseMongoEntity()
        {
            GenericAttributes = new List<GenericAttribute>();
        }

        public IList<GenericAttribute> GenericAttributes { get; set; }

    }
}
