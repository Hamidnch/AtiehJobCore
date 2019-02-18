using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Services.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace AtiehJobCore.Services.Users
{
    /// <inheritdoc />
    /// <summary>
    /// User attribute parser
    /// </summary>
    public partial class UserAttributeParser : IUserAttributeParser
    {
        private readonly IUserAttributeService _userAttributeService;
        private readonly ILocalizationService _localizationService;

        public UserAttributeParser(IUserAttributeService userAttributeService,
            ILocalizationService localizationService)
        {
            this._userAttributeService = userAttributeService;
            this._localizationService = localizationService;
        }

        /// <summary>
        /// Gets selected user attribute identifiers
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>Selected user attribute identifiers</returns>
        protected virtual IList<string> ParseUserAttributeIds(string attributesXml)
        {
            var ids = new List<string>();
            if (string.IsNullOrEmpty(attributesXml))
                return ids;

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(attributesXml);

                foreach (XmlNode node in xmlDoc.SelectNodes(@"//Attributes/UserAttribute"))
                {
                    if (node.Attributes?["ID"] == null)
                    {
                        continue;
                    }

                    var str1 = node.Attributes["ID"].InnerText.Trim();
                    ids.Add(str1);
                }
            }
            catch (Exception exc)
            {
                Debug.Write(exc.ToString());
            }
            return ids;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets selected user attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>Selected user attributes</returns>
        public virtual IList<UserAttribute> ParseUserAttributes(string attributesXml)
        {
            var result = new List<UserAttribute>();
            if (string.IsNullOrEmpty(attributesXml))
                return result;

            var ids = ParseUserAttributeIds(attributesXml);
            result.AddRange(ids.Select(id => _userAttributeService.GetUserAttributeById(id)).Where(attribute => attribute != null));
            return result;
        }

        /// <inheritdoc />
        /// <summary>
        /// Get user attribute values
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>User attribute values</returns>
        public virtual IList<UserAttributeValue> ParseUserAttributeValues(string attributesXml)
        {
            var values = new List<UserAttributeValue>();
            if (string.IsNullOrEmpty(attributesXml))
                return values;

            var attributes = ParseUserAttributes(attributesXml);
            foreach (var attribute in attributes)
            {
                if (!attribute.ShouldHaveValues())
                    continue;

                var valuesStr = ParseValues(attributesXml, attribute.Id);
                foreach (var valueStr in valuesStr)
                {
                    if (!string.IsNullOrEmpty(valueStr))
                    {
                        var value = attribute.UserAttributeValues.FirstOrDefault(x => x.Id == valueStr);
                        if (value != null)
                            values.Add(value);
                    }
                }
            }
            return values;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets selected user attribute value
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="userAttributeId">User attribute identifier</param>
        /// <returns>User attribute value</returns>
        public virtual IList<string> ParseValues(string attributesXml, string userAttributeId)
        {
            var selectedUserAttributeValues = new List<string>();
            if (string.IsNullOrEmpty(attributesXml))
                return selectedUserAttributeValues;

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(attributesXml);

                var nodeList1 = xmlDoc.SelectNodes(@"//Attributes/UserAttribute");
                foreach (XmlNode node1 in nodeList1)
                {
                    if (node1.Attributes?["ID"] == null)
                    {
                        continue;
                    }

                    var str1 = node1.Attributes["ID"].InnerText.Trim();
                    if (str1 != userAttributeId)
                    {
                        continue;
                    }

                    var nodeList2 = node1.SelectNodes(@"UserAttributeValue/Value");
                    foreach (XmlNode node2 in nodeList2)
                    {
                        var value = node2.InnerText.Trim();
                        selectedUserAttributeValues.Add(value);
                    }
                }
            }
            catch (Exception exc)
            {
                Debug.Write(exc.ToString());
            }
            return selectedUserAttributeValues;
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds an attribute
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="ca">User attribute</param>
        /// <param name="value">Value</param>
        /// <returns>Attributes</returns>
        public virtual string AddUserAttribute(string attributesXml, UserAttribute ca, string value)
        {
            var result = string.Empty;
            try
            {
                var xmlDoc = new XmlDocument();
                if (string.IsNullOrEmpty(attributesXml))
                {
                    var element1 = xmlDoc.CreateElement("Attributes");
                    xmlDoc.AppendChild(element1);
                }
                else
                {
                    xmlDoc.LoadXml(attributesXml);
                }
                var rootElement = (XmlElement)xmlDoc.SelectSingleNode(@"//Attributes");

                XmlElement attributeElement = null;
                //find existing
                var nodeList1 = xmlDoc.SelectNodes(@"//Attributes/UserAttribute");
                if (nodeList1 != null)
                {
                    foreach (XmlNode node1 in nodeList1)
                    {
                        if (node1.Attributes?["ID"] == null)
                        {
                            continue;
                        }

                        var str1 = node1.Attributes["ID"].InnerText.Trim();
                        if (str1 != ca.Id)
                        {
                            continue;
                        }

                        attributeElement = (XmlElement)node1;
                        break;
                    }
                }

                //create new one if not found
                if (attributeElement == null)
                {
                    attributeElement = xmlDoc.CreateElement("UserAttribute");
                    attributeElement.SetAttribute("ID", ca.Id.ToString());
                    rootElement?.AppendChild(attributeElement);
                }

                var attributeValueElement = xmlDoc.CreateElement("UserAttributeValue");
                attributeElement.AppendChild(attributeValueElement);

                var attributeValueValueElement = xmlDoc.CreateElement("Value");
                attributeValueValueElement.InnerText = value;
                attributeValueElement.AppendChild(attributeValueValueElement);

                result = xmlDoc.OuterXml;
            }
            catch (Exception exc)
            {
                Debug.Write(exc.ToString());
            }
            return result;
        }

        /// <inheritdoc />
        /// <summary>
        /// Validates user attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>Warnings</returns>
        public virtual IList<string> GetAttributeWarnings(string attributesXml)
        {
            var warnings = new List<string>();

            //ensure it's our attributes
            var attributes1 = ParseUserAttributes(attributesXml);

            //validate required user attributes (whether they're chosen/selected/entered)
            var attributes2 = _userAttributeService.GetAllUserAttributes();
            foreach (var a2 in attributes2)
            {
                if (!a2.IsRequired)
                {
                    continue;
                }

                var found = false;
                //selected user attributes
                foreach (var a1 in attributes1)
                {
                    if (a1.Id != a2.Id)
                    {
                        continue;
                    }

                    var valuesStr = ParseValues(attributesXml, a1.Id);
                    if (valuesStr.Any(str1 => !string.IsNullOrEmpty(str1.Trim())))
                    {
                        found = true;
                    }
                }

                //if not found
                if (found)
                {
                    continue;
                }

                var notFoundWarning = string.Format(_localizationService.GetResource("ShoppingCart.SelectAttribute"),
                    a2.GetLocalized(a => a.Name));

                warnings.Add(notFoundWarning);
            }

            return warnings;
        }

    }
}
