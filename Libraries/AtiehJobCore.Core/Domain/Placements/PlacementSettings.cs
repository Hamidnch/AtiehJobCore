using AtiehJobCore.Core.Configuration;

namespace AtiehJobCore.Core.Domain.Placements
{
    public class PlacementSettings : ISettings
    {
        public bool IsDisplayLicenseNumber { get; set; }
        public bool IsOptionalLicenseNumber { get; set; }
        public bool AllowDuplicateLicenseNumber { get; set; }
    }
}
