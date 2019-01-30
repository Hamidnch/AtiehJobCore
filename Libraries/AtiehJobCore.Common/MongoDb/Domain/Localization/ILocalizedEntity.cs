using System.Collections.Generic;

namespace AtiehJobCore.Common.MongoDb.Domain.Localization
{
    /// <summary>
    /// Represents a localized entity
    /// </summary>
    public interface ILocalizedEntity
    {
        IList<LocalizedProperty> Locales { get; set; }
    }
}
