namespace IteratesBrewManager.Domain.Entities;
public class Brewery : BaseAuditableEntity
{
    public string Name { get; set; }
    public IList<Beer> Beers { get; private set; } = new List<Beer>();

    public Brewery()
    {

    }

    public Brewery(string name)
    {
        Name = name;
    }
}
