using AtiehJobCore.Common.Plugins;

namespace AtiehJobCore.Services.Authentication.External
{
    /// <inheritdoc />
    /// <summary>
    /// Represents method for the external authentication
    /// </summary>
    public partial interface IExternalAuthenticationMethod : IPlugin
    {
        /// <summary>
        /// Gets a view component for displaying plugin in public store
        /// </summary>
        /// <param name="viewComponentName">View component name</param>
        void GetPublicViewComponent(out string viewComponentName);
    }
}
