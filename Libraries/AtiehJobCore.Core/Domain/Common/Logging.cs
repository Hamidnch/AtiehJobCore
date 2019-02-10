using AtiehJobCore.Core.Domain.Logging;

namespace AtiehJobCore.Core.Domain.Common
{
    public class Logging
    {
        public bool IncludeScopes { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}
