using AtiehJobCore.Core.Domain.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace AtiehJobCore.Services.Messages
{
    /// <inheritdoc />
    /// <summary>
    /// Contact attribute parser
    /// </summary>
    public partial class ContactAttributeParser : IContactAttributeParser
    {
        private readonly IContactAttributeService _contactAttributeService;

        public ContactAttributeParser(IContactAttributeService contactAttributeService)
        {
            this._contactAttributeService = contactAttributeService;
        }

        /// <summary>
        /// Gets selected contact attribute identifiers
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>Selected contact attribute identifiers</returns>
        protected virtual IList<string> ParseContactAttributeIds(string attributesXml)
        {
            var ids = new List<string>();
            if (string.IsNullOrEmpty(attributesXml))
                return ids;

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(attributesXml);

                foreach (XmlNode node in xmlDoc.SelectNodes(@"//Attributes/ContactAttribute"))
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
        /// Gets selected contact attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>Selected contact attributes</returns>
        public virtual IList<ContactAttribute> ParseContactAttributes(string attributesXml)
        {
            var result = new List<ContactAttribute>();
            if (string.IsNullOrEmpty(attributesXml))
                return result;

            var ids = ParseContactAttributeIds(attributesXml);
            result.AddRange(ids.Select(id => _contactAttributeService.GetContactAttributeById(id)).Where(attribute => attribute != null));
            return result;
        }

        /// <inheritdoc />
        /// <summary>
        /// Get contact attribute values
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>Contact attribute values</returns>
        public virtual IList<ContactAttributeValue> ParseContactAttributeValues(string attributesXml)
        {
            var values = new List<ContactAttributeValue>();
            if (string.IsNullOrEmpty(attributesXml))
                return values;

            var attributes = ParseContactAttributes(attributesXml);
            foreach (var attribute in attributes)
            {
                if (!attribute.ShouldHaveValues())
                    continue;

                var valuesStr = ParseValues(attributesXml, attribute.Id);
                values.AddRange(from valueStr in valuesStr
                                where !string.IsNullOrEmpty(valueStr)
                                select attribute.ContactAttributeValues.FirstOrDefault(x => x.Id == valueStr)
                    into value
                                where value != null
                                select value);
            }
            return values;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets selected contact attribute value
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="contactAttributeId">Contact attribute identifier</param>
        /// <returns>Contact attribute value</returns>
        public virtual IList<string> ParseValues(string attributesXml, string contactAttributeId)
        {
            var selectedContactAttributeValues = new List<string>();
            if (string.IsNullOrEmpty(attributesXml))
                return selectedContactAttributeValues;

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(attributesXml);

                var nodeList1 = xmlDoc.SelectNodes(@"//Attributes/ContactAttribute");
                if (nodeList1 != null)
                {
                    foreach (XmlNode node1 in nodeList1)
                    {
                        if (node1.Attributes?["ID"] == null)
                        {
                            continue;
                        }

                        var str1 = node1.Attributes["ID"].InnerText.Trim();
                        if (str1 != contactAttributeId)
                        {
                            continue;
                        }

                        var nodeList2 = node1.SelectNodes(@"ContactAttributeValue/Value");
                        if (nodeList2 != null)
                        {
                            foreach (XmlNode node2 in nodeList2)
                            {
                                var value = node2.InnerText.Trim();
                                selectedContactAttributeValues.Add(value);
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Debug.Write(exc.ToString());
            }
            return selectedContactAttributeValues;
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds an attribute
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="ca">Contact attribute</param>
        /// <param name="value">Value</param>
        /// <returns>Attributes</returns>
        public virtual string AddContactAttribute(string attributesXml, ContactAttribute ca, string value)
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
                var nodeList1 = xmlDoc.SelectNodes(@"//Attributes/ContactAttribute");
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
                    attributeElement = xmlDoc.CreateElement("ContactAttribute");
                    attributeElement.SetAttribute("ID", ca.Id.ToString());
                    rootElement?.AppendChild(attributeElement);
                }

                var attributeValueElement = xmlDoc.CreateElement("ContactAttributeValue");
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
        /// Check whether condition of some attribute is met (if specified). Return "null" if not condition is specified
        /// </summary>
        /// <param name="attribute">Contact attribute</param>
        /// <param name="selectedAttributesXml">Selected attributes (XML format)</param>
        /// <returns>Result</returns>
        public virtual bool? IsConditionMet(ContactAttribute attribute, string selectedAttributesXml)
        {
            if (attribute == null)
                throw new ArgumentNullException(nameof(attribute));

            var conditionAttributeXml = attribute.ConditionAttributeXml;
            if (string.IsNullOrEmpty(conditionAttributeXml))
                //no condition
                return null;

            //load an attribute this one depends on
            var dependOnAttribute = ParseContactAttributes(conditionAttributeXml).FirstOrDefault();
            if (dependOnAttribute == null)
                return true;

            var valuesThatShouldBeSelected = ParseValues(conditionAttributeXml, dependOnAttribute.Id)
                //a workaround here:
                //ConditionAttributeXml can contain "empty" values (nothing is selected)
                //but in other cases (like below) we do not store empty values
                //that's why we remove empty values here
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();
            var selectedValues = ParseValues(selectedAttributesXml, dependOnAttribute.Id);
            if (valuesThatShouldBeSelected.Count != selectedValues.Count)
                return false;

            //compare values
            var allFound = true;
            foreach (var t1 in valuesThatShouldBeSelected)
            {
                var found = false;
                foreach (var t2 in selectedValues)
                    if (t1 == t2)
                        found = true;
                if (!found)
                    allFound = false;
            }

            return allFound;
        }

        /// <inheritdoc />
        /// <summary>
        /// Remove an attribute
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="attribute">Contact attribute</param>
        /// <returns>Updated result (XML format)</returns>
        public virtual string RemoveContactAttribute(string attributesXml, ContactAttribute attribute)
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
                var nodeList1 = xmlDoc.SelectNodes(@"//Attributes/CheckoutAttribute");
                if (nodeList1 != null)
                {
                    foreach (XmlNode node1 in nodeList1)
                    {
                        if (node1.Attributes?["ID"] == null)
                        {
                            continue;
                        }

                        var str1 = node1.Attributes["ID"].InnerText.Trim();
                        if (str1 != attribute.Id)
                        {
                            continue;
                        }

                        attributeElement = (XmlElement)node1;
                        break;
                    }
                }

                //found
                if (attributeElement != null)
                {
                    rootElement?.RemoveChild(attributeElement);
                }

                result = xmlDoc.OuterXml;
            }
            catch (Exception exc)
            {
                Debug.Write(exc.ToString());
            }
            return result;
        }
    }
}
