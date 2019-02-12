using AtiehJobCore.Core.Domain.Messages;
using AtiehJobCore.Core.Enums;

namespace AtiehJobCore.Services.Messages
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class ContactAttributeExtensions
    {
        /// <summary>
        /// Gets a value indicating whether this contact attribute should have values
        /// </summary>
        /// <param name="contactAttribute">Contact attribute</param>
        /// <returns>Result</returns>
        public static bool ShouldHaveValues(this ContactAttribute contactAttribute)
        {
            if (contactAttribute == null)
                return false;

            return contactAttribute.AttributeControlType != AttributeControlType.TextBox
                   && contactAttribute.AttributeControlType != AttributeControlType.MultilineTextBox
                   && contactAttribute.AttributeControlType != AttributeControlType.Datepicker
                   && contactAttribute.AttributeControlType != AttributeControlType.FileUpload;

            //other attribute control types support values
        }

        /// <summary>
        /// A value indicating whether this contact attribute can be used as condition for some other attribute
        /// </summary>
        /// <param name="contactAttribute">Contact attribute</param>
        /// <returns>Result</returns>
        public static bool CanBeUsedAsCondition(this ContactAttribute contactAttribute)
        {
            if (contactAttribute == null)
                return false;

            return contactAttribute.AttributeControlType != AttributeControlType.ReadonlyCheckboxes
                   && contactAttribute.AttributeControlType != AttributeControlType.TextBox
                   && contactAttribute.AttributeControlType != AttributeControlType.MultilineTextBox
                   && contactAttribute.AttributeControlType != AttributeControlType.Datepicker
                   && contactAttribute.AttributeControlType != AttributeControlType.FileUpload;

            //other attribute control types support it
        }
    }
}
