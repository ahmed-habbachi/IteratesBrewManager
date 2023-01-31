using IteratesBrewManager.Application.Common.Mappings;
using IteratesBrewManager.Domain.Entities;

namespace IteratesBrewManager.Application.Common.Models;

public class BeerDto : IMapFrom<Beer>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public double AlcoholContent { get; set; }

    public double Price { get; set; }

    public int BrewerId { get; set; }
}
