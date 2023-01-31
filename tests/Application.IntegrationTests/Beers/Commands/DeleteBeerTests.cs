using IteratesBrewManager.Application.Common.Exceptions;
using IteratesBrewManager.Application.Beers.Commands.CreateBeer;
using IteratesBrewManager.Application.Beers.Commands.DeleteBeer;
using IteratesBrewManager.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using IteratesBrewManager.Application.Breweries.Commands.CreateBrewery;

namespace IteratesBrewManager.Application.IntegrationTests.Beers.Commands;

using static Testing;

public class DeleteBeerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidBeerId()
    {
        var command = new DeleteBeerCommand(99);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteBeer()
    {
        var breweryId = await SendAsync(new CreateBreweryCommand
        {
            Name = "New Brewery"
        });

        var beerId = await SendAsync(new CreateBeerCommand
        {
            BrewerId = breweryId,
            Name = "beer one",
            AlcoholContent = 6,
            Price = 2.2
        });

        await SendAsync(new DeleteBeerCommand(beerId));

        var beer = await FindAsync<Beer>(beerId);

        beer.Should().BeNull();
    }
}
