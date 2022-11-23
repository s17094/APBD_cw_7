using Crawler.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        private readonly IClientsService _clientsService;

        public ClientsController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient(int idClient)
        {
            var deletedClient = await _clientsService.DeleteClient(idClient);
            return Ok(deletedClient);
        }

    }
}
