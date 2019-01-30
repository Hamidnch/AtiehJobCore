using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Html;

namespace AtiehJobCore.Web.Framework.Localization
{
    public class LocalizedString : HtmlString
    {
        private readonly string _localized;

        public LocalizedString(string localized) : base(localized)
        {
            _localized = localized;
        }

        public string Text
        {
            get { return _localized; }
        }
    }
}
