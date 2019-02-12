namespace AtiehJobCore.Core.Html.CodeFormatter
{
    /// <inheritdoc />
    /// <summary>
    /// Generates color-coded HTML 4.01 from MSH (code name Monad) source code.
    /// </summary>
    public partial class MshFormat : CodeFormat
    {
        /// <inheritdoc />
        /// <summary>
        /// Regular expression string to match single line comments (#).
        /// </summary>
        protected override string CommentRegex => @"#.*?(?=\r|\n)";

        /// <inheritdoc />
        /// <summary>
        /// Regular expression string to match string and character literals. 
        /// </summary>
		protected override string StringRegex => @"@?""""|@?"".*?(?!\\).""|''|'.*?(?!\\).'";

        /// <inheritdoc />
        /// <summary>
        /// The list of MSH keywords.
        /// </summary>
		protected override string Keywords =>
            "function filter global script local private if else"
            + " elseif for foreach in while switch continue break"
            + " return default param begin process end throw trap";

        /// <inheritdoc />
        /// <summary>
        /// Use preprocessors property to highlight operators.
        /// </summary>
		protected override string Preprocessors =>
            "-band -bor -match -notmatch -like -notlike -eq -ne"
            + " -gt -ge -lt -le -is -imatch -inotmatch -ilike"
            + " -inotlike -ieq -ine -igt -ige -ilt -ile";
    }
}
