using System;

namespace AtiehJobCore.Services.Infrastructure
{
    /// <inheritdoc />
    /// <summary>
    /// Default machine name provider
    /// </summary>
    public class DefaultMachineNameProvider : IMachineNameProvider
    {
        /// <inheritdoc />
        /// <summary>
        /// Returns the name of the machine (instance) running the application.
        /// </summary>
        public string GetMachineName()
        {
            return Environment.MachineName;
        }
    }
}
