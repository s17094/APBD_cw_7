using Crawler.Context;
using Crawler.Exceptions.Client;
using Crawler.Models;
using Microsoft.EntityFrameworkCore;

namespace Crawler.Services;

public class ClientsService : IClientsService
{

    private readonly MyDbContext _context;

    public ClientsService(MyDbContext context)
    {
        _context = context;
    }

    public async Task<Client> DeleteClient(int idClient)
    {
        var client = await _context.Clients
            .Where(s => s.IdClient == idClient)
            .Include(s => s.ClientTrips)
            .FirstOrDefaultAsync();

        if (client == null)
        {
            throw new ClientNotFoundException(idClient);
        }

        if (client.ClientTrips.Any())
        {
            throw new ClientRemoveException(idClient);
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return client;
    }
}