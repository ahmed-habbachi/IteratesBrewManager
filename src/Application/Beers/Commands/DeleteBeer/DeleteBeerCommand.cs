using IteratesBrewManager.Application.Common.Exceptions;
using IteratesBrewManager.Application.Common.Interfaces;
using IteratesBrewManager.Domain.Entities;
using MediatR;

namespace IteratesBrewManager.Application.Beers.Commands.DeleteBeer;

public record DeleteBeerCommand(int Id) : IRequest;

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
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Beer), request.Id);
        }

        _context.Beers.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
