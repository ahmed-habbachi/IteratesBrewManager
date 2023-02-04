using IteratesBrewManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IteratesBrewManager.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_context.Breweries.Any())
        {
            Brewery brewer = new("Abbaye de Leffe");
            Brewery brewerTwo = new("SFBT");

            _context.Breweries.Add(brewer);
            _context.Breweries.Add(brewerTwo);

            await _context.SaveChangesAsync();

            Beer newBeer = new Beer("Leffe Blonde", 2.2, brewer)
            {
                AlcoholContent = 6.6
            };

            Beer newBeerTwo = new Beer("Celtia", 3.2, brewer)
            {
                AlcoholContent = 6.6
            };

            Beer newBeerThree = new Beer("Heineken", 3.2, brewerTwo)
            {
                AlcoholContent = 6.6
            };

            _context.Beers.Add(newBeer);
            _context.Beers.Add(newBeerTwo);
            _context.Beers.Add(newBeerThree);

            await _context.SaveChangesAsync();

            Wholesaler wholesaler = new("GeneDrinks");
            _context.Wholesalers.Add(wholesaler);
            await _context.SaveChangesAsync();

            _context.WholesalerBeerStocks.Add(new(wholesaler, newBeer, 10));
            _context.WholesalerBeerStocks.Add(new(wholesaler, newBeerTwo, 40));
            _context.WholesalerBeerStocks.Add(new(wholesaler, newBeerThree, 60));
            await _context.SaveChangesAsync();
        }
    }
}
