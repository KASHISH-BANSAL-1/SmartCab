using SmartCab.Core.Models;
using FluentAssertions;

namespace SmartCab.Tests
{
    [TestClass]
    public class CabTests
    {
        [TestMethod]
        public void AddEarnings_ShouldIncreaseTotalEarnings()
        {
            var cab = new Cab("CAB1", new Location(0, 0));
            cab.AddEarnings(100);
            cab.TotalEarnings.Should().Be(100);
        }

        [TestMethod]
        public void AddEarnings_ShouldAccumulateMultipleValues()
        {
            var cab = new Cab("CAB1", new Location(0, 0));
            cab.AddEarnings(100);
            cab.AddEarnings(50);
            cab.TotalEarnings.Should().Be(150);
        }

        [TestMethod]
        public void UpdateLocation_ShouldChangeCurrentLocation()
        {
            var cab = new Cab("CAB1", new Location(0, 0));
            var newLocation = new Location(5, 5);
            cab.UpdateLocation(newLocation);
            cab.CurrentLocation.X.Should().Be(5);
            cab.CurrentLocation.Y.Should().Be(5);
        }

        [TestMethod]
        public void Cab_ShouldBeAvailable_ByDefault()
        {
            var cab = new Cab("CAB1", new Location(0, 0));
            cab.IsAvailable.Should().BeTrue();
        }

        [TestMethod]
        public void Cab_ShouldBeMarkedUnavailable_Manually()
        {
            var cab = new Cab("CAB1", new Location(0, 0));
            cab.SetStatus(CabStatus.Booked);
            cab.IsAvailable.Should().BeFalse();
        }
    }
}