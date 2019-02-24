using AtiehJobCore.Core.Configuration;

namespace AtiehJobCore.Core.Domain.Employers
{
    public class EmployerSettings : ISettings
    {
        public bool IsDisplayInsuranceCode { get; set; }
        public bool IsOptionalInsuranceCode { get; set; }
        public bool AllowDuplicateInsuranceCode { get; set; }
    }
}
