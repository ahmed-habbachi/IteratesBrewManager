using IteratesBrewManager.Application.Common.Exceptions;
using IteratesBrewManager.Application.Common.Interfaces;
using IteratesBrewManager.Domain.Entities;
using MediatR;

namespace IteratesBrewManager.Application.Breweries.Commands.DeleteBrewery;

public record DeleteBreweryCommand(int Id) : IRequest;

public class DeleteBreweryCommandHandler : IRequestHandler<DeleteBreweryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteBreweryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteBreweryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Breweries
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Brewery), request.Id);
        }

        _context.Breweries.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
