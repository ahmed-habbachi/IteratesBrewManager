using IteratesBrewManager.Application.Common.Interfaces;
using IteratesBrewManager.Application.Common.Models;
using IteratesBrewManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IteratesBrewManager.Application.Wholesalers.Commands.RequestQuote;

public record RequestQuoteCommand : IRequest<QuoteRequestResult>
{
    public bool SaveRequest { get; set; }
    public int WholesalerId { get; set; }
    public BeerQuantityPairDto[] BeerQuantityPair { get; set; }
}

public class RequestQuoteCommandHandler : IRequestHandler<RequestQuoteCommand, QuoteRequestResult>
{
    private readonly IApplicationDbContext _context;

    public RequestQuoteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<QuoteRequestResult> Handle(RequestQuoteCommand request, CancellationToken cancellationToken)
    {
        var wholesalerBeerStockList = await _context.WholesalerBeerStocks.Include(stock=> stock.Beer).Include(stock => stock.Wholesaler).Where(wb => wb.WholesalerId == request.WholesalerId).ToListAsync(cancellationToken);

        int drinksCount = 0;
        double drinksPrice = 0;

        var entity = new Quote();

        foreach (var beerQuantityPair in request.BeerQuantityPair)
        {
            var wholesalerBeerStock = wholesalerBeerStockList.Find(stock => stock.BeerId == beerQuantityPair.BeerId);

            if (wholesalerBeerStock != null && wholesalerBeerStock.Stock >= beerQuantityPair.Quantity)
            {
                entity.Wholesaler = wholesalerBeerStock.Wholesaler;

                var quateItem = new QuoteItem();
                entity.QuoteItems.Add(quateItem);
                quateItem.Beer = wholesalerBeerStock.Beer;
                quateItem.BeerPrice= wholesalerBeerStock.Beer.Price;
                quateItem.Quantity = beerQuantityPair.Quantity;
                quateItem.Quote = entity;
                drinksCount += beerQuantityPair.Quantity;
                drinksPrice += beerQuantityPair.Quantity * quateItem.BeerPrice;
            }
        }

        if (drinksCount > 20)
        {
            entity.PriceToPay = drinksPrice - (drinksPrice * 20 / 100);
        }
        else if (drinksCount > 10)
        {
            entity.PriceToPay = drinksPrice - (drinksPrice * 10 / 100);
        }
        else
        {
            entity.PriceToPay = drinksPrice;
        }

        if (request.SaveRequest)
        {
            _context.Quotes.Add(entity);
            await _context.SaveChangesAsync(cancellationToken); 
        }

        return new QuoteRequestResult(GetQuoteDto(entity));
    }

    private QuoteDto GetQuoteDto(Quote entity)
    {
        var quoteDto = new QuoteDto()
        {
            WholesalerId= entity.WholesalerId,
            WholesalerName= entity.Wholesaler.Name,
            PriceToPay= entity.PriceToPay
        };

        foreach (var quoteItem in entity.QuoteItems)
        {
            quoteDto.QuoteItems.Add(new QuoteItemDto(quoteItem.Beer.Name, quoteItem.BeerPrice, quoteItem.Quantity));
        }

        return quoteDto;
    }
}
