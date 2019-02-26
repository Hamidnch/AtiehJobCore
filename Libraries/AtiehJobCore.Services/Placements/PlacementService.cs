using AtiehJobCore.Core.Domain.Placements;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Events;
using System;

namespace AtiehJobCore.Services.Placements
{
    public class PlacementService : IPlacementService
    {
        private readonly IRepository<Placement> _placementRepository;
        private readonly IEventPublisher _eventPublisher;

        public PlacementService(IRepository<Placement> placementRepository, IEventPublisher eventPublisher)
        {
            _placementRepository = placementRepository;
            _eventPublisher = eventPublisher;
        }

        public Placement InsertPlacement(Placement placement)
        {
            if (placement == null)
                throw new ArgumentNullException(nameof(placement));

            var currentPlacement = _placementRepository.Insert(placement);

            //event notification
            _eventPublisher.EntityInserted(placement);
            return currentPlacement;
        }

        public bool IsNotDuplicateLicenseNumber(string licenseNumber)
        {
            var r = _placementRepository.Any(e => e.LicenseNumber == licenseNumber);
            return !r;
        }
    }
}
