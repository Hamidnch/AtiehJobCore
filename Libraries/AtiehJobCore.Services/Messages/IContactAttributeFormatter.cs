using AtiehJobCore.Core.Domain.Users;

namespace AtiehJobCore.Services.Messages
{
    /// <summary>
    /// Contact attribute helper
    /// </summary>
    public partial interface IContactAttributeFormatter
    {
        /// <summary>
        /// Formats attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>Attributes</returns>
        string FormatAttributes(string attributesXml);

        /// <summary>
        /// Formats attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="user">User</param>
        /// <param name="separator">Separator</param>
        /// <param name="htmlEncode">A value indicating whether to encode (HTML) values</param>
        /// <param name="allowHyperlinks">A value indicating whether to HTML hyperlink tags could be rendered (if required)</param>
        /// <returns>Attributes</returns>
        string FormatAttributes(string attributesXml,
            User user,
            string separator = "<br />",
            bool htmlEncode = true,
            bool allowHyperlinks = true);
    }
}
