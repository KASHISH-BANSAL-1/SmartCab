using SmartCab.Core.Interfaces;
using SmartCab.Core.Models;

namespace SmartCab.Core.Services
{
    public class DispatchService : IDispatchService
    {
        private readonly ICabRepository _cabRepository;
        private readonly IFareCalculator _fareCalculator;

        public DispatchService(ICabRepository cabRepository, IFareCalculator fareCalculator)
        {
            _cabRepository = cabRepository;
            _fareCalculator = fareCalculator;
        }

        public Cab AssignCab(RideRequest request)
        {
            var availableCabs = _cabRepository.GetAllCabs()
              .Where(c => c.IsAvailable)
              .ToList();

            if (!availableCabs.Any())
                return null;

            var nearestCab = availableCabs
                .OrderBy(c => c.CurrentLocation.GetManhattanDistance(request.PickupLocation))
                .First();

            decimal fare = _fareCalculator.CalculateFare(request.PickupLocation, request.DropoffLocation);
            nearestCab.AddEarnings(fare);
            nearestCab.UpdateLocation(request.DropoffLocation);

            return nearestCab;
        }

        public void CompleteRide(Cab cab, RideRequest request)
        {
            cab.UpdateLocation(request.DropoffLocation);
            var fare = _fareCalculator.CalculateFare(request.PickupLocation, request.DropoffLocation);
            cab.AddEarnings(fare);
            cab.SetStatus(CabStatus.Available);
            _cabRepository.UpdateCab(cab);
        }
    }
}