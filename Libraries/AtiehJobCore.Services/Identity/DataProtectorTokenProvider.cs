using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AtiehJobCore.Services.Identity
{
    public class ConfirmEmailDataProtectionTokenProviderOptions : DataProtectionTokenProviderOptions
    { }

    /// <inheritdoc />
    /// <summary>
    /// How to override the default (1 day) TokenLifeSpan for the email confirmations.
    /// </summary>
    public class DataProtectorTokenProvider<TUser> :
        Microsoft.AspNetCore.Identity.DataProtectorTokenProvider<TUser> where TUser : class
    {
        public DataProtectorTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<ConfirmEmailDataProtectionTokenProviderOptions> options)
            : base(dataProtectionProvider, options)
        {
            // NOTE: DataProtectionTokenProviderOptions.TokenLifespan is set to TimeSpan.FromDays(1)
            // which is low for the `ConfirmEmail` task.
        }
    }
}