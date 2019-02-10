using AtiehJobCore.Core.Domain.Localization;
using AtiehJobCore.Core.Domain.Users;

namespace AtiehJobCore.Core.Contracts
{
    public interface IWorkContext
    {
        User CurrentUser { get; set; }

        Language WorkingLanguage { get; set; }
    }
}
