namespace SmartCab.API.Dtos
{
    public class RideRequestDto
    {
        public LocationDto Pickup { get; set; }
        public LocationDto Dropoff { get; set; }
    }
}
