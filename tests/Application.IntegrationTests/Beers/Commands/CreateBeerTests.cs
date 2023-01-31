using IteratesBrewManager.Application.Common.Exceptions;
using IteratesBrewManager.Application.Beers.Commands.CreateBeer;
using IteratesBrewManager.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using IteratesBrewManager.Application.Breweries.Commands.CreateBrewery;

namespace IteratesBrewManager.Application.IntegrationTests.Beers.Commands;

using static Testing;

public class CreateBeerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateBeerCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateBeer()
    {
        var breweryId = await SendAsync(new CreateBreweryCommand
        {
            Name = "New Brewery"
        });

        var command = new CreateBeerCommand
        {
            BrewerId = breweryId,
            Name = "beer one",
            AlcoholContent = 6,
            Price = 2.2
        };

        var beerId = await SendAsync(command);

        var beer = await FindAsync<Beer>(beerId);

        beer.Should().NotBeNull();
        beer!.BrewerId.Should().Be(command.BrewerId);
        beer.Name.Should().Be(command.Name);
        beer.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        beer.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
