using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.Html;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Services.Media;
using System;
using System.Linq;
using System.Net;
using System.Text;

namespace AtiehJobCore.Services.Messages
{
    /// <inheritdoc />
    /// <summary>
    /// Contact attribute helper
    /// </summary>
    public partial class ContactAttributeFormatter : IContactAttributeFormatter
    {
        private readonly IWorkContext _workContext;
        private readonly IContactAttributeService _contactAttributeService;
        private readonly IContactAttributeParser _contactAttributeParser;
        private readonly IDownloadService _downloadService;
        private readonly IWebHelper _webHelper;

        public ContactAttributeFormatter(IWorkContext workContext,
            IContactAttributeService contactAttributeService,
            IContactAttributeParser contactAttributeParser,
            IDownloadService downloadService,
            IWebHelper webHelper)
        {
            this._workContext = workContext;
            this._contactAttributeService = contactAttributeService;
            this._contactAttributeParser = contactAttributeParser;
            this._downloadService = downloadService;
            this._webHelper = webHelper;
        }

        /// <inheritdoc />
        /// <summary>
        /// Formats attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>Attributes</returns>
        public virtual string FormatAttributes(string attributesXml)
        {
            var user = _workContext.CurrentUser;
            return FormatAttributes(attributesXml, user);
        }

        /// <inheritdoc />
        /// <summary>
        /// Formats attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="user">User</param>
        /// <param name="separator">Separator</param>
        /// <param name="htmlEncode">A value indicating whether to encode (HTML) values</param>
        /// <param name="allowHyperlinks">A value indicating whether to HTML hyperink tags could be rendered (if required)</param>
        /// <returns>Attributes</returns>
        public virtual string FormatAttributes(string attributesXml,
            User user,
            string separator = "<br />",
            bool htmlEncode = true,
            bool allowHyperlinks = true)
        {
            var result = new StringBuilder();

            var attributes = _contactAttributeParser.ParseContactAttributes(attributesXml);
            for (var i = 0; i < attributes.Count; i++)
            {
                var attribute = attributes[i];
                var valuesStr = _contactAttributeParser.ParseValues(attributesXml, attribute.Id);
                for (var j = 0; j < valuesStr.Count; j++)
                {
                    var valueStr = valuesStr[j];
                    var formattedAttribute = "";
                    if (!attribute.ShouldHaveValues())
                    {
                        switch (attribute.AttributeControlType)
                        {
                            //no values
                            case AttributeControlType.MultilineTextBox:
                                {
                                    //multiline TextBox
                                    var attributeName = attribute.GetLocalized(a => a.Name, _workContext.WorkingLanguage.Id);
                                    //encode (if required)
                                    if (htmlEncode)
                                        attributeName = WebUtility.HtmlEncode(attributeName);
                                    formattedAttribute =
                                        $"{attributeName}: {HtmlHelper.FormatText(valueStr, false, true, false, false, false, false)}";
                                    //we never encode multiline TextBox input
                                    break;
                                }
                            case AttributeControlType.FileUpload:
                                {
                                    //file upload
                                    Guid.TryParse(valueStr, out var downloadGuid);
                                    var download = _downloadService.GetDownloadByGuid(downloadGuid);
                                    if (download != null)
                                    {
                                        //TODO add a method for getting URL (use routing because it handles all SEO friendly URLs)
                                        var attributeText = "";
                                        var fileName =
                                            $"{download.Filename ?? download.DownloadGuid.ToString()}{download.Extension}";
                                        //encode (if required)
                                        if (htmlEncode)
                                            fileName = WebUtility.HtmlEncode(fileName);
                                        if (allowHyperlinks)
                                        {
                                            //hyperlinks are allowed
                                            var downloadLink =
                                                $"{_webHelper.GetLocation(false)}download/getfileupload/?downloadId={download.DownloadGuid}";
                                            attributeText =
                                                $"<a href=\"{downloadLink}\" class=\"fileuploadattribute\">{fileName}</a>";
                                        }
                                        else
                                        {
                                            //hyperlinks aren't allowed
                                            attributeText = fileName;
                                        }
                                        var attributeName = attribute.GetLocalized(a => a.Name, _workContext.WorkingLanguage.Id);
                                        //encode (if required)
                                        if (htmlEncode)
                                            attributeName = WebUtility.HtmlEncode(attributeName);
                                        formattedAttribute = $"{attributeName}: {attributeText}";
                                    }

                                    break;
                                }
                            case AttributeControlType.DropdownList:
                                break;
                            case AttributeControlType.RadioList:
                                break;
                            case AttributeControlType.Checkboxes:
                                break;
                            case AttributeControlType.TextBox:
                                break;
                            case AttributeControlType.Datepicker:
                                break;
                            case AttributeControlType.ColorSquares:
                                break;
                            case AttributeControlType.ImageSquares:
                                break;
                            case AttributeControlType.ReadonlyCheckboxes:
                                break;
                            default:
                                {
                                    //other attributes (textbox, datepicker)
                                    formattedAttribute =
                                        $"{attribute.GetLocalized(a => a.Name, _workContext.WorkingLanguage.Id)}: {valueStr}";
                                    //encode (if required)
                                    if (htmlEncode)
                                        formattedAttribute = WebUtility.HtmlEncode(formattedAttribute);
                                    break;
                                }
                        }
                    }
                    else
                    {
                        var attributeValue = attribute.ContactAttributeValues.FirstOrDefault(x => x.Id == valueStr);
                        if (attributeValue != null)
                        {
                            formattedAttribute =
                                $"{attribute.GetLocalized(a => a.Name, _workContext.WorkingLanguage.Id)}: {attributeValue.GetLocalized(a => a.Name, _workContext.WorkingLanguage.Id)}";
                        }
                        //encode (if required)
                        if (htmlEncode)
                            formattedAttribute = WebUtility.HtmlEncode(formattedAttribute);
                    }

                    if (string.IsNullOrEmpty(formattedAttribute))
                    {
                        continue;
                    }

                    if (i != 0 || j != 0)
                        result.Append(separator);
                    result.Append(formattedAttribute);
                }
            }

            return result.ToString();
        }
    }
}
