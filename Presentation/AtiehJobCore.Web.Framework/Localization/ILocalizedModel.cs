using System.Collections.Generic;

namespace AtiehJobCore.Web.Framework.Localization
{
    public interface ILocalizedModel
    {
    }
    public interface ILocalizedModel<T> : ILocalizedModel
    {
        IList<T> Locales { get; set; }
    }
}
