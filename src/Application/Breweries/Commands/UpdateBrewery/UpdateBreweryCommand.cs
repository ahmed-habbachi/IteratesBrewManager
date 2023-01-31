using IteratesBrewManager.Application.Common.Exceptions;
using IteratesBrewManager.Application.Common.Interfaces;
using IteratesBrewManager.Domain.Entities;
using MediatR;

namespace IteratesBrewManager.Application.Breweries.Commands.UpdateBrewery;

public record UpdateBreweryCommand : IRequest
{
    public int Id { get; init; }

    public string Name { get; set; }
}

public class UpdateBreweryCommandHandler : IRequestHandler<UpdateBreweryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBreweryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateBreweryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Breweries
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Brewery), request.Id);
        }

        entity.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
