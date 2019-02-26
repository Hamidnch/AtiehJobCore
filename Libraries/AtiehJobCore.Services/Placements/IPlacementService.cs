using AtiehJobCore.Core.Domain.Placements;

namespace AtiehJobCore.Services.Placements
{
    public interface IPlacementService
    {
        Placement InsertPlacement(Placement employer);
        bool IsNotDuplicateLicenseNumber(string insuranceCode);
    }
}
