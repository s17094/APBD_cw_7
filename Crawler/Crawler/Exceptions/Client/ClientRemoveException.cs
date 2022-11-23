using Crawler.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.Exceptions.Client;

public class ClientRemoveException : ClientException
{
    private readonly int _clientId;

    public ClientRemoveException(int clientId)
    {
        _clientId = clientId;
    }

    protected internal override IActionResult GetResponse()
    {
        var message = new ErrorMessageDto("Failed to remove client with id = " + _clientId + ". " +
                                          "It has assigned trips. Unassigned them first.");
        return new ObjectResult(message)
        {
            StatusCode = 400
        };
    }
}