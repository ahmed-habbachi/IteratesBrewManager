using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratesBrewManager.Domain.Entities;
public class Beer : BaseAuditableEntity
{
    public string Name { get; set; }
    public double AlcoholContent { get; set; }
    public double Price { get; set; }

    public int BrewerId { get; set; }
    public Brewery Brewer { get; set; }

    public Beer()
    {

    }

    public Beer(string name, double price, Brewery brewer)
    {
        Name = name;
        Price = price;
        Brewer = brewer;
        BrewerId= brewer.Id;
    }
}
