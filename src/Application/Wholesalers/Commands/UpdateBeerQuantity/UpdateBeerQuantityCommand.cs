using IteratesBrewManager.Application.Common.Interfaces;
using IteratesBrewManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IteratesBrewManager.Application.Wholesalers.Commands.UpdateBeerQuantity;

public record UpdateBeerQuantityCommand : IRequest<int>
{
    public int WholesalerId { get; set; }
    public int BeerId { get; set; }
    public int Quantity { get; set; }
}

public class UpdateBeerQuantityCommandHandler : IRequestHandler<UpdateBeerQuantityCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdateBeerQuantityCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateBeerQuantityCommand request, CancellationToken cancellationToken)
    {
        var wholesalerBeer = await _context.WholesalerBeerStocks.SingleAsync(wb => wb.BeerId == request.BeerId && wb.WholesalerId == request.WholesalerId, cancellationToken);

        wholesalerBeer.Stock += request.Quantity;

        await _context.SaveChangesAsync(cancellationToken);

        return wholesalerBeer.Stock;
    }
}
