using AtiehJobCore.Core.Domain.Localization;
using AtiehJobCore.Core.MongoDb;
using System.Collections.Generic;

namespace AtiehJobCore.Core.Domain.Users
{
    /// <inheritdoc>
    ///     <cref></cref>
    /// </inheritdoc>
    /// <summary>
    /// Represents a user attribute value
    /// </summary>
    public partial class UserAttributeValue : SubBaseMongoEntity, ILocalizedEntity
    {
        public UserAttributeValue()
        {
            Locales = new List<LocalizedProperty>();
        }
        /// <summary>
        /// Gets or sets the user attribute identifier
        /// </summary>
        public string UserAttributeId { get; set; }

        /// <summary>
        /// Gets or sets the checkout attribute name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the value is pre-selected
        /// </summary>
        public bool IsPreSelected { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the collection of locales
        /// </summary>
        public IList<LocalizedProperty> Locales { get; set; }

    }
}
