using IteratesBrewManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IteratesBrewManager.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Brewery> Breweries { get; }

    DbSet<Beer> Beers { get; }

    DbSet<Wholesaler> Wholesalers { get; }

    DbSet<WholesalerBeerStock> WholesalerBeerStocks { get; }

    DbSet<Sale> Sales { get; }
    DbSet<Quote> Quotes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
