using SmartCab.Core.Models;
using SmartCab.Core.Services;
using SmartCab.Core.Interfaces;
using NSubstitute;
using FluentAssertions;

namespace SmartCab.Tests
{
    [TestClass]
    public class DispatchServiceTests
    {
        [TestMethod]
        public void AssignCab_ShouldAssignNearestAvailableCab()
        {
            var cab1 = new Cab("CAB1", new Location(0, 0));
            var cab2 = new Cab("CAB2", new Location(10, 10));
            var cabs = new List<Cab> { cab1, cab2 };

            var cabRepo = Substitute.For<ICabRepository>();
            cabRepo.GetAllCabs().Returns(cabs);

            var fareCalculator = Substitute.For<IFareCalculator>();
            fareCalculator.CalculateFare(Arg.Any<Location>(), Arg.Any<Location>()).Returns(100);

            var dispatchService = new DispatchService(cabRepo, fareCalculator);
            var request = new RideRequest(new Location(1, 1), new Location(5, 5));

            var assignedCab = dispatchService.AssignCab(request);

            assignedCab.Should().Be(cab1);
            assignedCab.CurrentLocation.X.Should().Be(5);
            assignedCab.CurrentLocation.Y.Should().Be(5);
            assignedCab.TotalEarnings.Should().Be(100);
            assignedCab.IsAvailable.Should().BeTrue();
        }

        [TestMethod]
        public void AssignCab_ShouldReturnNull_IfNoAvailableCabs()
        {
            var cab1 = new Cab("CAB1", new Location(0, 0));
            cab1.SetStatus(CabStatus.Booked);
            var cabRepo = Substitute.For<ICabRepository>();
            cabRepo.GetAllCabs().Returns(new List<Cab> { cab1 });

            var fareCalculator = Substitute.For<IFareCalculator>();
            var dispatchService = new DispatchService(cabRepo, fareCalculator);
            var request = new RideRequest(new Location(1, 1), new Location(5, 5));

            var assignedCab = dispatchService.AssignCab(request);

            assignedCab.Should().BeNull();
        }

        [TestMethod]
        public void CompleteRide_ShouldMarkCabAsAvailable_AndUpdateLocation()
        {
            var cab = new Cab("CAB1", new Location(0, 0));
            cab.SetStatus(CabStatus.Booked);
            var request = new RideRequest(new Location(1, 1), new Location(2, 2));

            var cabRepo = Substitute.For<ICabRepository>();
            var fareCalculator = Substitute.For<IFareCalculator>();

            var service = new DispatchService(cabRepo, fareCalculator);
            service.CompleteRide(cab, request);

            cab.IsAvailable.Should().BeTrue();
            cab.CurrentLocation.X.Should().Be(2);
            cab.CurrentLocation.Y.Should().Be(2);
        }
    }
}
