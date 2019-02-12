using AtiehJobCore.Core.Html.CodeFormatter;
using System.Text.RegularExpressions;

namespace AtiehJobCore.Core.Html
{
    /// <summary>
    /// Represents a BBCode helper
    /// </summary>
    public static class BbCodeHelper
    {
        #region Fields
        private static readonly Regex RegexBold = new Regex(@"\[b\](.+?)\[/b\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexItalic = new Regex(@"\[i\](.+?)\[/i\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexUnderLine = new Regex(@"\[u\](.+?)\[/u\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexUrl1 = new Regex(@"\[url\=([^\]]+)\]([^\]]+)\[/url\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexUrl2 = new Regex(@"\[url\](.+?)\[/url\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexQuote = new Regex(@"\[quote=(.+?)\](.+?)\[/quote\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexImg = new Regex(@"\[img\](.+?)\[/img\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #endregion

        #region Methods
        /// <summary>
        /// Formats the text
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="replaceBold">A value indicating whether to replace Bold</param>
        /// <param name="replaceItalic">A value indicating whether to replace Italic</param>
        /// <param name="replaceUnderline">A value indicating whether to replace Underline</param>
        /// <param name="replaceUrl">A value indicating whether to replace URL</param>
        /// <param name="replaceCode">A value indicating whether to replace Code</param>
        /// <param name="replaceQuote">A value indicating whether to replace Quote</param>
        /// <param name="replaceImg">A value indicating whether to replace Img</param>
        /// <returns>Formatted text</returns>
        public static string FormatText(string text, bool replaceBold, bool replaceItalic,
            bool replaceUnderline, bool replaceUrl, bool replaceCode, bool replaceQuote, bool replaceImg)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (replaceBold)
            {
                // format the bold tags: [b][/b]
                // becomes: <strong></strong>
                text = RegexBold.Replace(text, "<strong>$1</strong>");
            }

            if (replaceItalic)
            {
                // format the italic tags: [i][/i]
                // becomes: <em></em>
                text = RegexItalic.Replace(text, "<em>$1</em>");
            }

            if (replaceUnderline)
            {
                // format the underline tags: [u][/u]
                // becomes: <u></u>
                text = RegexUnderLine.Replace(text, "<u>$1</u>");
            }

            if (replaceUrl)
            {
                text = RegexUrl1.Replace(text, "<a href=\"$1\" rel=\"nofollow\">$2</a>");

                text = RegexUrl2.Replace(text, "<a href=\"$1\" rel=\"nofollow\">$1</a>");
            }

            if (replaceQuote)
            {
                while (RegexQuote.IsMatch(text))
                    text = RegexQuote.Replace(text, "<b>$1 wrote:</b><div class=\"quote\">$2</div>");
            }

            if (replaceCode)
            {
                text = CodeFormatHelper.FormatTextSimple(text);
            }

            if (replaceImg)
            {
                text = RegexImg.Replace(text, "<img src=\"$1\" class=\"user-posted-image\" alt=\"\"></img>");
            }
            return text;
        }

        /// <summary>
        /// Removes all quotes from string
        /// </summary>
        /// <param name="str">Source string</param>
        /// <returns>string</returns>
        public static string RemoveQuotes(string str)
        {
            str = Regex.Replace(str, @"\[quote=(.+?)\]", string.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\[/quote\]", string.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return str;
        }

        #endregion
    }
}
