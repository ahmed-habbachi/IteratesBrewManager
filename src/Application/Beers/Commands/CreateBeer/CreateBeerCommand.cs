using IteratesBrewManager.Application.Common.Interfaces;
using IteratesBrewManager.Domain.Entities;
using MediatR;

namespace IteratesBrewManager.Application.Beers.Commands.CreateBeer;

public record CreateBeerCommand : IRequest<int>
{
    public int BrewerId { get; set; }
    public string Name { get; set; }
    public double AlcoholContent { get; set; }
    public double Price { get; set; }
}

public class CreateBeerCommandHandler : IRequestHandler<CreateBeerCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateBeerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateBeerCommand request, CancellationToken cancellationToken)
    {
        var brewer = await _context.Breweries.FindAsync(new object[] { request.BrewerId }, cancellationToken);

        if (brewer == null)
        {
            // TODO: throw new exception brewer not found
            return 0;
        }

        var entity = new Beer(request.Name, request.Price, brewer)
        {
            AlcoholContent= request.AlcoholContent,
        };

        _context.Beers.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
