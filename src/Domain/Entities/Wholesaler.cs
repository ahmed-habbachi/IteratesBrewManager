using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratesBrewManager.Domain.Entities;
public class Wholesaler : BaseAuditableEntity
{
    public string Name { get; set; }
    public IList<WholesalerBeer> WholesalerBeerItems { get; private set; } = new List<WholesalerBeer>();

    public Wholesaler()
    {

    }

    public Wholesaler(string name)
    {
        Name = name;
    }
}
