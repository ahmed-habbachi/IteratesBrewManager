using IteratesBrewManager.Application.Common.Mappings;
using IteratesBrewManager.Domain.Entities;

namespace IteratesBrewManager.Application.Common.Models;

public class BreweryDto : IMapFrom<Brewery>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public IList<BeerDto> Beers { get; private set; } = new List<BeerDto>();
}
