using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Enums;

namespace AtiehJobCore.Services.Users
{
    public static class UserAttributeExtensions
    {
        /// <summary>
        /// A value indicating whether this user attribute should have values
        /// </summary>
        /// <param name="userAttribute">User attribute</param>
        /// <returns>Result</returns>
        public static bool ShouldHaveValues(this UserAttribute userAttribute)
        {
            if (userAttribute == null)
                return false;

            return userAttribute.AttributeControlType != AttributeControlType.TextBox
                   && userAttribute.AttributeControlType != AttributeControlType.MultilineTextBox
                   && userAttribute.AttributeControlType != AttributeControlType.Datepicker
                   && userAttribute.AttributeControlType != AttributeControlType.FileUpload;

            //other attribute control types support values
        }
    }
}
