using Crawler.Models.Dto;

namespace Crawler.Services;

public interface ITripsService
{
    Task<List<TripsResponseDto>> GetTrips();
    Task<AssignClientToTripResponseDto> AssignClientToTrip(int idTrip, AssignClientToTripRequestDto requestDto);
}