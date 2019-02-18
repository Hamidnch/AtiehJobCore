using AtiehJobCore.Core.Domain.Localization;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.MongoDb;
using System.Collections.Generic;

namespace AtiehJobCore.Core.Domain.Users
{
    /// <inheritdoc>
    ///     <cref></cref>
    /// </inheritdoc>
    /// <summary>
    /// Represents a user attribute
    /// </summary>
    public partial class UserAttribute : BaseMongoEntity, ILocalizedEntity
    {
        private ICollection<UserAttributeValue> _userAttributeValues;

        public UserAttribute()
        {
            Locales = new List<LocalizedProperty>();
        }
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the attribute is required
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the attribute control type identifier
        /// </summary>
        public int AttributeControlTypeId { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the collection of locales
        /// </summary>
        public IList<LocalizedProperty> Locales { get; set; }


        /// <summary>
        /// Gets the attribute control type
        /// </summary>
        public AttributeControlType AttributeControlType
        {
            get => (AttributeControlType)this.AttributeControlTypeId;
            set => this.AttributeControlTypeId = (int)value;
        }
        /// <summary>
        /// Gets the user attribute values
        /// </summary>
        public virtual ICollection<UserAttributeValue> UserAttributeValues
        {
            get => _userAttributeValues ?? (_userAttributeValues = new List<UserAttributeValue>());
            protected set => _userAttributeValues = value;
        }
    }
}
