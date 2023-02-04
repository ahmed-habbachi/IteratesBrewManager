using IteratesBrewManager.Application.Common.Exceptions;
using IteratesBrewManager.Application.Common.Interfaces;
using IteratesBrewManager.Domain.Entities;
using MediatR;

namespace IteratesBrewManager.Application.Breweries.Commands.DeleteBeer;

public record DeleteBeerCommand(int brewerId, int Beerid) : IRequest;

public class DeleteBeerCommandHandler : IRequestHandler<DeleteBeerCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteBeerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteBeerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Beers
            .FindAsync(new object[] { request.Beerid }, cancellationToken);

        if (entity == null || entity.BrewerId != request.brewerId)
        {
            throw new NotFoundException(nameof(Beer), request.Beerid);
        }

        _context.Beers.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
