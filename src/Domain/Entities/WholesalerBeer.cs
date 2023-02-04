using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratesBrewManager.Domain.Entities;
public class WholesalerBeerStock : BaseAuditableEntity
{
    public int WholesalerId { get; set; }
    public Wholesaler Wholesaler { get; set; }
    public int BeerId { get; set; }
    public Beer Beer { get; set; }
    public int Stock { get; set; }

    public WholesalerBeerStock()
    {

    }

    public WholesalerBeerStock(Wholesaler wholesaler, Beer beer, int stock)
    {
        WholesalerId = wholesaler.Id;
        Wholesaler = wholesaler;
        BeerId = beer.Id;
        Beer = beer;
        Stock = stock;
    }
}
