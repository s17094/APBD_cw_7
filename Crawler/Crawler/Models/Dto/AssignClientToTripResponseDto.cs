namespace Crawler.Models.Dto;

public class AssignClientToTripResponseDto
{
    public AssignClientToTripResponseDto(int clientId, int tripId)
    {
        Message = "Client with id = " + clientId + " has been assigned to trip with id = " + tripId;
    }

    public string Message { get; }
}