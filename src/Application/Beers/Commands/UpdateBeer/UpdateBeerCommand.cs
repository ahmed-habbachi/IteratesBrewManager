using IteratesBrewManager.Application.Common.Exceptions;
using IteratesBrewManager.Application.Common.Interfaces;
using IteratesBrewManager.Domain.Entities;
using MediatR;

namespace IteratesBrewManager.Application.Beers.Commands.UpdateBeer;

public record UpdateBeerCommand : IRequest
{
    public int Id { get; init; }

    public string Name { get; set; }
    public double AlcoholContent { get; set; }
    public double Price { get; set; }
    public int BrewerId { get; set; }
}

public class UpdateBeerCommandHandler : IRequestHandler<UpdateBeerCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBeerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateBeerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Beers
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Beer), request.Id);
        }

        entity.Name = request.Name;
        entity.AlcoholContent = request.AlcoholContent;
        entity.Price = request.Price;
        entity.BrewerId = request.BrewerId;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
