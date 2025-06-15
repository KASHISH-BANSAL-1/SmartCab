namespace SmartCab.Core.Models
{
    public class Cab
    {
        public string Id { get; }
        public Location CurrentLocation { get; private set; }
        public CabStatus Status { get; private set; } = CabStatus.Available;
        public decimal TotalEarnings { get; private set; }
        public bool IsAvailable => Status == CabStatus.Available;

        public Cab(string id, Location location)
        {
            Id = id;
            CurrentLocation = location;
        }


        public void UpdateLocation(Location newLocation) => CurrentLocation = newLocation;
        public void AddEarnings(decimal fare) => TotalEarnings += fare;
        public void SetStatus(CabStatus status) => Status = status;



    }
}