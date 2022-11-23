using Crawler.Models.Dto;
using Crawler.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripsController : ControllerBase
    {

        private readonly ITripsService _tripsService;

        public TripsController(ITripsService tripsService)
        {
            _tripsService = tripsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _tripsService.GetTrips();
            return Ok(trips);
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AssignClientToTrip(int idTrip, AssignClientToTripRequestDto requestDto)
        {
            var assigned = await _tripsService.AssignClientToTrip(idTrip, requestDto);
            return Ok(assigned);
        }

    }
}
