using System.Collections.Generic;
using AtiehJobCore.Core.Domain.Common;

namespace AtiehJobCore.Core.MongoDb
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
