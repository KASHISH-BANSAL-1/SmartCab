using SmartCab.Core.Models;
using SmartCab.Core.Services;
using FluentAssertions;

namespace SmartCab.Tests
{
    [TestClass]
    public class FareCalculatorTests
    {
        [TestMethod]
        public void CalculateFare_ShouldReturnBaseFare_WhenPickupEqualsDropoff()
        {
            var calculator = new FareCalculator();
            var location = new Location(3, 3);
            var fare = calculator.CalculateFare(location, location);
            fare.Should().Be(50);
        }

        [TestMethod]
        public void CalculateFare_ShouldReturnCorrectFare_ForGivenDistance()
        {
            var calculator = new FareCalculator();
            var pickup = new Location(1, 2);
            var dropoff = new Location(4, 6);
            var fare = calculator.CalculateFare(pickup, dropoff);
            fare.Should().Be(120); // 50 base + (5 distance * 20)
        }

        [TestMethod]
        public void CalculateFare_ShouldHandleZeroDistance()
        {
            var calculator = new FareCalculator();
            var same = new Location(0, 0);
            var fare = calculator.CalculateFare(same, same);
            fare.Should().Be(50);
        }

        [TestMethod]
        public void CalculateFare_ShouldCalculateCorrectManhattanFare()
        {
            var calculator = new FareCalculator();
            var pickup = new Location(0, 0);
            var dropoff = new Location(2, 2);

            var expectedFare = 50 + (4 * 10); // 4 is Manhattan distance

            var fare = calculator.CalculateFare(pickup, dropoff);

            fare.Should().Be(expectedFare);
        }

    }
}