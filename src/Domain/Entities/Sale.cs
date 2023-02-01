namespace IteratesBrewManager.Domain.Entities;
public class Sale: BaseAuditableEntity
{
    public int WholesalerBeerItemId { get; set; }
    public WholesalerBeer WholesalerBeerItem { get; set; }
    public int Quantity { get; set; }

}
