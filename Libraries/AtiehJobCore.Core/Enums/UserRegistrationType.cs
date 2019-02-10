namespace AtiehJobCore.Core.Enums
{
    /// <summary>
    /// Represents the customer registration type formatting enumeration
    /// </summary>
    public enum UserRegistrationType
    {
        /// <summary>
        /// Standard account creation
        /// </summary>
        Standard = 1,
        /// <summary>
        /// A customer should be approved by administrator
        /// </summary>
        AdminApproval = 2,
        /// <summary>
        /// Email validation is required after registration
        /// </summary>
        EmailValidation = 3,
        /// <summary>
        /// Mobile validation is required after registration
        /// </summary>
        MobileValidation = 4,
        /// <summary>
        /// Registration is disabled
        /// </summary>
        Disabled = 5
    }
}
