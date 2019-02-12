using AtiehJobCore.Core.Configuration;

namespace AtiehJobCore.Core.Domain.Messages
{
    public class EmailAccountSettings : ISettings
    {
        /// <summary>
        /// Gets or sets a store default email account identifier
        /// </summary>
        public string DefaultEmailAccountId { get; set; }

    }
}
