using System.Collections.Generic;
using AtiehJobCore.Common.MongoDb.Common;

namespace AtiehJobCore.Common.MongoDb.Data
{
    public abstract partial class BaseMongoEntity : ParentEntity
    {
        protected BaseMongoEntity()
        {
            GenericAttributes = new List<GenericAttribute>();
        }

        public IList<GenericAttribute> GenericAttributes { get; set; }

    }
}
