using Microsoft.AspNetCore.Mvc;

namespace Crawler.Exceptions.Trip;

public abstract class TripException : Exception
{
    protected internal abstract IActionResult GetResponse();
}