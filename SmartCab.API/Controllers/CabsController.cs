using Microsoft.AspNetCore.Mvc;
using SmartCab.Core.Interfaces;

namespace SmartCab.API.Controllers
{
    [ApiController]
    [Route("api/cabs")]
    public class CabsController : ControllerBase
    {
        private readonly ICabRepository _cabRepository;

        public CabsController(ICabRepository cabRepository)
        {
            _cabRepository = cabRepository;
        }

        [HttpGet("{id}/status")]
        public IActionResult GetCabStatus(string id)
        {
            var cab = _cabRepository.GetCabById(id);
            if (cab == null) return NotFound("Cab not found");

            return Ok(new { cab.Id, cab.IsAvailable, cab.CurrentLocation, cab.TotalEarnings });
        }

        [HttpGet("{id}/history")]
        public IActionResult GetCabHistory(string id)
        {
            // Assuming history is not implemented, just return stub
            return Ok(new[] {
                new { RideId = "RIDE123", From = "Loc A", To = "Loc B", Fare = 100 },
                new { RideId = "RIDE124", From = "Loc C", To = "Loc D", Fare = 80 }
            });
        }
    }
}