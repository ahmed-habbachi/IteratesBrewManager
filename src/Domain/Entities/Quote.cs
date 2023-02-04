namespace IteratesBrewManager.Domain.Entities;
public class Quote: BaseAuditableEntity
{
    public int WholesalerId { get; set; }
    public Wholesaler Wholesaler { get; set; }
    public double PriceToPay { get; set; }
    public IList<QuoteItem> QuoteItems { get; private set; } = new List<QuoteItem>();
}

public class QuoteItem : BaseAuditableEntity
{
    public int QuoteId { get; set; }
    public Quote Quote { get; set; }
    public int BeerId { get; set; }
    public Beer Beer { get; set; }
    public double BeerPrice { get; set; }
    public int Quantity { get; set; }
}
