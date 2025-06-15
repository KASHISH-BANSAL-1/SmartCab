using Microsoft.AspNetCore.Mvc;
using SmartCab.API.Dtos;
using SmartCab.Core.Interfaces;
using SmartCab.Core.Models;
using SmartCab.Core.Services;

namespace SmartCab.API.Controllers
{
    [ApiController]
    [Route("api/rides")]
    public class RidesController : ControllerBase
    {
        private readonly IDispatchService _dispatchService;

        public RidesController(IDispatchService dispatchService)
        {
            _dispatchService = dispatchService;
        }

        [HttpPost("request")]
        public IActionResult RequestRide([FromBody] RideRequestDto dto)
        {
            var request = new RideRequest(
                new Location(dto.Pickup.X, dto.Pickup.Y),
                new Location(dto.Dropoff.X, dto.Dropoff.Y)
            );

            var cab = _dispatchService.AssignCab(request);

            if (cab == null)
                return NotFound("No available cab at the moment.");

            return Ok(new
            {
                cab.Id,
                cab.CurrentLocation,
                cab.TotalEarnings,
                cab.IsAvailable
            });
        }
    }
}