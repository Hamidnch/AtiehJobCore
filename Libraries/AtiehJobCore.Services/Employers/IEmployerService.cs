using AtiehJobCore.Core.Domain.Employers;

namespace AtiehJobCore.Services.Employers
{
    public interface IEmployerService
    {
        Employer InsertEmployer(Employer employer);
        bool IsNotDuplicateInsuranceCode(string insuranceCode);
    }
}
