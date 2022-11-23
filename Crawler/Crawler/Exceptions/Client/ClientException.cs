using Microsoft.AspNetCore.Mvc;

namespace Crawler.Exceptions.Client;

public abstract class ClientException : Exception
{
    protected internal abstract IActionResult GetResponse();
}