using FluentValidation;
using IteratesBrewManager.Application.Common.Interfaces;
using IteratesBrewManager.Application.Common.Models;
using IteratesBrewManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IteratesBrewManager.Application.Wholesalers.Commands.RequestQuote;

public class RequestQuoteCommandValidator : AbstractValidator<RequestQuoteCommand>
{
    private readonly IApplicationDbContext _context;

    public RequestQuoteCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.BeerQuantityPair)
            .NotNull().DependentRules(() =>
            {
                RuleFor(v => v.BeerQuantityPair.Length)
                    .GreaterThan(0).WithMessage("The order cannot be empty");

                RuleFor(v => v.BeerQuantityPair)
                    .Must(beNoDuplicateBeer).WithMessage("There can't be any duplicate in the order");

                RuleFor(v => v)
                    .MustAsync(beBeerSoldByWholesaler).DependentRules(() =>
                    {
                        RuleFor(v => v)
                            .MustAsync(beBeerAvailableInStock).WithMessage("The number of beers ordered cannot be greater than the wholesaler's stock");
                    }).WithMessage("The beer must be sold by the wholesaler");
            }).WithMessage("The order cannot be empty");

        RuleFor(v => v.WholesalerId)
            .GreaterThanOrEqualTo(0)
            .MustAsync(beValidWholesaler).WithMessage("The wholesaler must exist");
    }

    private async Task<bool> beBeerSoldByWholesaler(RequestQuoteCommand request, CancellationToken cancellationToken)
    {
        var wholesalerBeerStockList = await _context.WholesalerBeerStocks.Include(stock => stock.Beer).Include(stock => stock.Wholesaler).Where(wb => wb.WholesalerId == request.WholesalerId).ToListAsync(cancellationToken);

        foreach (var beerQuantityPair in request.BeerQuantityPair)
        {
            if (!wholesalerBeerStockList.Any(stock => stock.BeerId == beerQuantityPair.BeerId))
            {
                return false;
            }
        }

        return true;
    }

    private async Task<bool> beBeerAvailableInStock(RequestQuoteCommand request, CancellationToken cancellationToken)
    {
        var wholesalerBeerStockList = await _context.WholesalerBeerStocks.Include(stock => stock.Beer).Include(stock => stock.Wholesaler).Where(wb => wb.WholesalerId == request.WholesalerId).ToListAsync(cancellationToken);

        foreach (var beerQuantityPair in request.BeerQuantityPair)
        {
            var wholesalerBeerStock = wholesalerBeerStockList.Find(stock => stock.BeerId == beerQuantityPair.BeerId);

            if (wholesalerBeerStock?.Stock < beerQuantityPair.Quantity)
            { 
                return false;
            }
        }

        return true;
    }

    private bool beNoDuplicateBeer(BeerQuantityPairDto[] BeerQuantityPair)
    {
        return !BeerQuantityPair.GroupBy(x => new { x.BeerId }).Where(x => x.Skip(1).Any()).Any();
    }

    private async Task<bool> beValidWholesaler(int WholesalerId, CancellationToken cancellationToken)
    {
        var wholesaler = await _context.Wholesalers.FindAsync(new object[] { WholesalerId }, cancellationToken);
        return wholesaler!= null;
    }
}
