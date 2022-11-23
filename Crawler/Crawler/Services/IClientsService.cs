using Crawler.Models;

namespace Crawler.Services;

public interface IClientsService
{
    Task<Client> DeleteClient(int idClient);
}