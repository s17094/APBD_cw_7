using Crawler.Exceptions.Client;
using Crawler.Exceptions.Trip;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Crawler.Filters;

public class RestExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ClientException clientException)
        {
            context.Result = clientException.GetResponse();
        }
        if (context.Exception is TripException tripException)
        {
            context.Result = tripException.GetResponse();
        }
    }
}