using IteratesBrewManager.Application.Common.Interfaces;
using IteratesBrewManager.Domain.Entities;
using MediatR;

namespace IteratesBrewManager.Application.Breweries.Commands.CreateBrewery;

public record CreateBreweryCommand : IRequest<int>
{
    public string Name { get; set; }
}

public class CreateBreweryCommandHandler : IRequestHandler<CreateBreweryCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateBreweryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateBreweryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Brewery(request.Name);

        _context.Breweries.Add(new Brewery(request.Name));

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
