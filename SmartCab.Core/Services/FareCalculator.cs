using SmartCab.Core.Interfaces;
using SmartCab.Core.Models;

namespace SmartCab.Core.Services
{
    public class FareCalculator : IFareCalculator
    {
        private const decimal BaseFare = 50;
        private const decimal PerUnitFare = 10;
        public decimal CalculateFare(Location pickup, Location dropoff)
        {
            int distance = pickup.GetManhattanDistance(dropoff);
            return BaseFare + (distance * PerUnitFare);
        }
    }
}