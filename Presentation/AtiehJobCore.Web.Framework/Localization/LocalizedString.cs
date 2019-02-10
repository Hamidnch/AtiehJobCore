using Microsoft.AspNetCore.Html;

namespace AtiehJobCore.Web.Framework.Localization
{
    public class LocalizedString : HtmlString
    {
        public LocalizedString(string localized) : base(localized)
        {
            Text = localized;
        }

        public string Text { get; }
    }
}
