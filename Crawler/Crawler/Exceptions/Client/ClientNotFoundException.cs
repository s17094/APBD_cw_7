using Crawler.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.Exceptions.Client;

public class ClientNotFoundException : ClientException
{
    private readonly int _clientId;

    public ClientNotFoundException(int clientId)
    {
        _clientId = clientId;
    }

    protected internal override IActionResult GetResponse()
    {
        var message = new ErrorMessageDto("Not found client with id = " + _clientId);
        return new ObjectResult(message)
        {
            StatusCode = 404
        };
    }
}