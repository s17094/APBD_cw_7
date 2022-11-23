using Crawler.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.Exceptions.Trip;

public class TripNotFoundException : TripException
{
    private readonly int _tripId;

    public TripNotFoundException(int tripId)
    {
        _tripId = tripId;
    }

    protected internal override IActionResult GetResponse()
    {
        var message = new ErrorMessageDto("Not found trip with id = " + _tripId);
        return new ObjectResult(message)
        {
            StatusCode = 404
        };
    }
}