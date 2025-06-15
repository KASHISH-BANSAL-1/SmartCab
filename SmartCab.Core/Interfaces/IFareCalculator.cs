using SmartCab.Core.Models;

namespace SmartCab.Core.Interfaces
{
    public interface IFareCalculator
    {
        decimal CalculateFare(Location pickup, Location dropoff);
    }
}