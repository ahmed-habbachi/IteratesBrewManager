using System.Reflection;
using IteratesBrewManager.Application.Common.Interfaces;
using IteratesBrewManager.Domain.Entities;
using IteratesBrewManager.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IteratesBrewManager.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) 
        : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Brewery> Breweries => Set<Brewery>();
     
    public DbSet<Beer> Beers => Set<Beer>();
     
    public DbSet<Wholesaler> Wholesalers => Set<Wholesaler>();
    
    public DbSet<WholesalerBeer> WholesalerBeerItems => Set<WholesalerBeer>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
