using Crawler.Context;
using Crawler.Exceptions.Trip;
using Crawler.Models;
using Crawler.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Crawler.Services;

public class TripsService : ITripsService
{

    private readonly MyDbContext _context;

    public TripsService(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<TripsResponseDto>> GetTrips()
    {
        return await _context.Trips
            .Select(trip => new TripsResponseDto
            {
                Name = trip.Name,
                Description = trip.Description,
                DateFrom = trip.DateFrom,
                DateTo = trip.DateTo,
                MaxPeople = trip.MaxPeople,
                Countries = trip.IdCountries.Select(country => new CountryResponseDto
                {
                    Name = country.Name
                }),
                Clients = trip.ClientTrips.Select(clientTrip => new ClientResponseDto
                {
                    FirstName = clientTrip.IdClientNavigation.FirstName,
                    LastName = clientTrip.IdClientNavigation.LastName
                })
            })
            .OrderByDescending(trip => trip.DateFrom)
            .ToListAsync();
    }

    public async Task<AssignClientToTripResponseDto> AssignClientToTrip(int idTrip, AssignClientToTripRequestDto requestDto)
    {
        var client = await GetClient(requestDto);

        CheckIfClientIsNotAlreadyAssignedToTrip(client, idTrip);

        var trip = await GetTripIfExists(idTrip);

        await AddClientToTrip(requestDto, client, trip);

        return new AssignClientToTripResponseDto(client.IdClient, idTrip);
    }

    private async Task<Client> GetClient(AssignClientToTripRequestDto requestDto)
    {
        var client = await GetClientWithPesel(requestDto.Pesel);

        if (client == null)
        {
            client = CreateNewClient(requestDto);
            _context.Clients.Add(client);
        }
        else
        {
            _context.Clients.Update(client);
        }

        return client;
    }

    private async Task<Client?> GetClientWithPesel(string pesel)
    {
        return await _context.Clients
            .Where(row => row.Pesel == pesel)
            .Include(client => client.ClientTrips)
            .FirstOrDefaultAsync();
    }

    private Client CreateNewClient(AssignClientToTripRequestDto requestDto)
    {
        return new Client
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            Email = requestDto.Email,
            Telephone = requestDto.Telephone,
            Pesel = requestDto.Pesel
        };
    }

    private void CheckIfClientIsNotAlreadyAssignedToTrip(Client client, int idTrip)
    {
        if (client.ClientTrips.Any(row => row.IdTrip == idTrip))
        {
            throw new ClientAlreadyAssignedToTripException(client.IdClient, idTrip);
        }
    }

    private async Task<Trip> GetTripIfExists(int idTrip)
    {
        var trip = await _context.Trips
            .FirstOrDefaultAsync(trip => trip.IdTrip == idTrip);
        if (trip == null)
        {
            throw new TripNotFoundException(idTrip);
        }

        return trip;
    }

    private async Task AddClientToTrip(AssignClientToTripRequestDto requestDto, Client client,
        Trip trip)
    {
        client.ClientTrips.Add(new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = trip.IdTrip,
            PaymentDate = requestDto.PaymentDate,
            RegisteredAt = DateTime.Now,
            IdClientNavigation = client,
            IdTripNavigation = trip
        });

        await _context.SaveChangesAsync();
    }

}