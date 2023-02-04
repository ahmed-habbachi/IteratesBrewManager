using IteratesBrewManager.Application.Common.Exceptions;
using IteratesBrewManager.Application.Beers.Commands.CreateBeer;
using IteratesBrewManager.Application.Beers.Commands.UpdateBeer;
using IteratesBrewManager.Application.Breweries.Commands.CreateBrewery;
using IteratesBrewManager.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace IteratesBrewManager.Application.IntegrationTests.Beers.Commands;

using static Testing;

public class UpdateBeerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidBeerId()
    {
        var command = new UpdateBeerCommand { Id = 99, Name = "New Beer", BrewerId = 1 };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateBeer()
    {
        var breweryId = await SendAsync(new CreateBreweryCommand
        {
            Name = "New Brewery"
        });

        var beerId = await SendAsync(new CreateBeerCommand
        {
            BrewerId = breweryId,
            Name = "beer one",
            AlcoholContent = 1,
            Price = 1.1
        });

        var command = new UpdateBeerCommand
        {
            Id = beerId,
            BrewerId = breweryId,
            Name = "beer two",
            AlcoholContent = 2,
            Price = 2.2
        };

        await SendAsync(command);

        var beer = await FindAsync<Beer>(beerId);

        beer.Should().NotBeNull();
        beer!.Name.Should().Be(command.Name);
        beer!.AlcoholContent.Should().Be(command.AlcoholContent);
        beer!.Price.Should().Be(command.Price);
        beer.LastModified.Should().NotBeNull();
        beer.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
