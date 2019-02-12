using System.Net;
using System.Text.RegularExpressions;

namespace AtiehJobCore.Core.Html.CodeFormatter
{
    /// <summary>
    /// Represents a code format helper
    /// </summary>
    public static class CodeFormatHelper
    {
        #region Fields
        private static readonly Regex RegexHtml = new Regex("<[^>]*>", RegexOptions.Compiled);
        private static readonly Regex RegexCode2 = new Regex(@"\[code\](?<inner>(.*?))\[/code\]",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
        #endregion

        #region Utilities

        /// <summary>
        /// Code evaluator method
        /// </summary>
        /// <param name="match">Match</param>
        /// <returns>Formatted text</returns>
        private static string CodeEvaluator(Match match)
        {
            if (!match.Success)
                return match.Value;

            var options = new HighlightOptions
            {
                Language = match.Groups["lang"].Value,
                Code = match.Groups["code"].Value,
                DisplayLineNumbers = match.Groups["linenumbers"].Value == "on",
                Title = match.Groups["title"].Value,
                AlternateLineNumbers = match.Groups["altlinenumbers"].Value == "on"
            };


            var result = match.Value.Replace(match.Groups["begin"].Value, "");
            result = result.Replace(match.Groups["end"].Value, "");
            result = Highlight(options, result);
            return result;

        }

        /// <summary>
        /// Code evaluator method
        /// </summary>
        /// <param name="match">Match</param>
        /// <returns>Formatted text</returns>
        private static string CodeEvaluatorSimple(Match match)
        {
            if (!match.Success)
                return match.Value;

            var options = new HighlightOptions
            {
                Language = "c#",
                Code = match.Groups["inner"].Value,
                DisplayLineNumbers = false,
                Title = string.Empty,
                AlternateLineNumbers = false
            };


            var result = match.Value;
            result = Highlight(options, result);
            return result;

        }

        /// <summary>
        /// Strips HTML
        /// </summary>
        /// <param name="html">HTML</param>
        /// <returns>Formatted text</returns>
        private static string StripHtml(string html)
        {
            return string.IsNullOrEmpty(html)
                ? string.Empty
                : RegexHtml.Replace(html, string.Empty);
        }

        /// <summary>
        /// Returns the formatted text.
        /// </summary>
        /// <param name="options">Whatever options were set in the regex groups.</param>
        /// <param name="text">Send the e.body so it can get formatted.</param>
        /// <returns>The formatted string of the match.</returns>
        private static string Highlight(HighlightOptions options, string text)
        {
            switch (options.Language)
            {
                case "c#":
                    var csf = new CSharpFormat();
                    csf.LineNumbers = options.DisplayLineNumbers;
                    csf.Alternate = options.AlternateLineNumbers;
                    return WebUtility.HtmlDecode(csf.FormatCode(text));

                case "vb":
                    var vbf = new VisualBasicFormat();
                    vbf.LineNumbers = options.DisplayLineNumbers;
                    vbf.Alternate = options.AlternateLineNumbers;
                    return vbf.FormatCode(text);

                case "js":
                    var jsf = new JavaScriptFormat();
                    jsf.LineNumbers = options.DisplayLineNumbers;
                    jsf.Alternate = options.AlternateLineNumbers;
                    return WebUtility.HtmlDecode(jsf.FormatCode(text));

                case "html":
                    var htmlf = new HtmlFormat();
                    htmlf.LineNumbers = options.DisplayLineNumbers;
                    htmlf.Alternate = options.AlternateLineNumbers;
                    text = StripHtml(text).Trim();
                    var code = htmlf.FormatCode(WebUtility.HtmlDecode(text)).Trim();
                    return code.Replace("\r\n", "<br />").Replace("\n", "<br />");

                case "xml":
                    var xmlf = new HtmlFormat();
                    xmlf.LineNumbers = options.DisplayLineNumbers;
                    xmlf.Alternate = options.AlternateLineNumbers;
                    text = text.Replace("<br />", "\r\n");
                    text = StripHtml(text).Trim();
                    var xml = xmlf.FormatCode(WebUtility.HtmlDecode(text)).Trim();
                    return xml.Replace("\r\n", "<br />").Replace("\n", "<br />");

                case "msh":
                    var mshf = new MshFormat();
                    mshf.LineNumbers = options.DisplayLineNumbers;
                    mshf.Alternate = options.AlternateLineNumbers;
                    return WebUtility.HtmlDecode(mshf.FormatCode(text));

            }

            return string.Empty;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Formats the text
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>Formatted text</returns>
        public static string FormatTextSimple(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (!text.Contains("[/code]"))
            {
                return text;
            }

            text = RegexCode2.Replace(text, new MatchEvaluator(CodeEvaluatorSimple));
            text = RegexCode2.Replace(text, "$1");
            return text;
        }

        #endregion
    }
}
