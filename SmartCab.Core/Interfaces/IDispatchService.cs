using SmartCab.Core.Models;

namespace SmartCab.Core.Interfaces
{
    public interface IDispatchService
    {
        Cab AssignCab(RideRequest request);
        void CompleteRide(Cab cab, RideRequest request);
    }
}