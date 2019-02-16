using AtiehJobCore.Core.Enums;
using System.Collections.Generic;

namespace AtiehJobCore.Web.Framework.Models.User
{
    public partial class UserAttributeModel : BaseMongoEntityModel
    {
        public UserAttributeModel()
        {
            Values = new List<UserAttributeValueModel>();
        }

        public string Name { get; set; }

        public bool IsRequired { get; set; }

        /// <summary>
        /// Default value for textboxes
        /// </summary>
        public string DefaultValue { get; set; }

        public AttributeControlType AttributeControlType { get; set; }

        public IList<UserAttributeValueModel> Values { get; set; }

    }

    public partial class UserAttributeValueModel : BaseMongoEntityModel
    {
        public string Name { get; set; }

        public bool IsPreSelected { get; set; }
    }
}
