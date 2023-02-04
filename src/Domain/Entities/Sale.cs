namespace IteratesBrewManager.Domain.Entities;
public class Sale: BaseAuditableEntity
{
    public int WholesalerBeerItemId { get; set; }
    public WholesalerBeerStock WholesalerBeerItem { get; set; }
    public int Quantity { get; set; }

}
