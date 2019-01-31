using AtiehJobCore.Common.MongoDb.Domain.Localization;
using AtiehJobCore.Common.MongoDb.Domain.Users;

namespace AtiehJobCore.Common.Infrastructure.MongoDb
{
    public interface IWorkContext
    {
        User CurrentUser { get; set; }

        Language WorkingLanguage { get; set; }
    }
}
