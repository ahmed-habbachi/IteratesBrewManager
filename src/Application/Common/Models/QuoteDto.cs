using IteratesBrewManager.Application.Common.Mappings;
using IteratesBrewManager.Domain.Common;
using IteratesBrewManager.Domain.Entities;

namespace IteratesBrewManager.Application.Common.Models;

public class QuoteDto
{
    public int WholesalerId { get; set; }
    public string WholesalerName { get; set; }
    public double PriceToPay { get; set; }
    public IList<QuoteItemDto> QuoteItems { get; private set; } = new List<QuoteItemDto>();

    public QuoteDto()
    {

    }
}

public class QuoteItemDto
{
    public string BeerName { get; set; }
    public double BeerPrice { get; set; }
    public int Quantity { get; set; }

    public QuoteItemDto(string beerName, double beerPrice, int quantity)
    {
        BeerName = beerName;
        BeerPrice = beerPrice;
        Quantity = quantity;
    }
}