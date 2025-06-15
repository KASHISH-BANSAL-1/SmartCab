namespace SmartCab.Core.Models
{
    public class RideRequest
    {
        public Location PickupLocation { get; }
        public Location DropoffLocation { get; }
        public RideRequest(Location pickup, Location dropoff) => (PickupLocation, DropoffLocation) = (pickup, dropoff);
    }
}