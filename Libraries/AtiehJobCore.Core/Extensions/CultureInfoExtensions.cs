using System;
using System.Globalization;
using AtiehJobCore.Core.Localization;

namespace AtiehJobCore.Core.Extensions
{
    public static class CultureInfoExtensions
    {
        public static LanguageDirection GetLanguageDirection(this CultureInfo cultureInfo)
        {
            if (cultureInfo == null)
            {
                throw new ArgumentNullException(nameof(cultureInfo));
            }

            return cultureInfo.TextInfo.IsRightToLeft ? LanguageDirection.Rtl : LanguageDirection.Ltr;
        }
    }
}
