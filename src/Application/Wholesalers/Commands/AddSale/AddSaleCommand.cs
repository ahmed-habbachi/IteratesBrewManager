using IteratesBrewManager.Application.Common.Interfaces;
using IteratesBrewManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IteratesBrewManager.Application.Wholesalers.Commands.AddSale;

public record AddSaleCommand : IRequest<int>
{
    public int WholesalerId { get; set; }
    public int BeerId { get; set; }
    public int Quantity { get; set; }
}

public class AddSaleCommandHandler : IRequestHandler<AddSaleCommand, int>
{
    private readonly IApplicationDbContext _context;

    public AddSaleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddSaleCommand request, CancellationToken cancellationToken)
    {
        var wholesalerBeer = await _context.WholesalerBeerItems.SingleAsync(wb => wb.BeerId == request.BeerId && wb.WholesalerId == request.WholesalerId, cancellationToken);

        var entity = new Sale();
        entity.WholesalerBeerItem = wholesalerBeer;
        entity.WholesalerBeerItemId = wholesalerBeer.Id;
        entity.Quantity = request.Quantity;

        wholesalerBeer.Stock -= request.Quantity;

        _context.Sales.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
