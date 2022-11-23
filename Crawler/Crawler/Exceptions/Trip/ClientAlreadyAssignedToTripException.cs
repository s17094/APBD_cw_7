using Crawler.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.Exceptions.Trip;

public class ClientAlreadyAssignedToTripException : TripException
{
    private readonly int _clientId;
    private readonly int _tripId;

    public ClientAlreadyAssignedToTripException(int clientId, int tripId)
    {
        _clientId = clientId;
        _tripId = tripId;
    }

    protected internal override IActionResult GetResponse()
    {
        var message = new ErrorMessageDto("Client with id = " + _clientId + " is already assigned to trip with id = " + _tripId);
        return new ObjectResult(message)
        {
            StatusCode = 400
        };
    }
}