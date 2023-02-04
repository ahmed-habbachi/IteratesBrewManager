using IteratesBrewManager.Application.Common.Exceptions;
using IteratesBrewManager.Application.Breweries.Commands.CreateBrewery;
using IteratesBrewManager.Application.Breweries.Commands.DeleteBrewery;
using IteratesBrewManager.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace IteratesBrewManager.Application.IntegrationTests.Brewerys.Commands;

using static Testing;

public class DeleteBreweryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidBreweryId()
    {
        var command = new DeleteBreweryCommand(99);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteBrewery()
    {
        var breweryId = await SendAsync(new CreateBreweryCommand
        {
            Name = "New Brewery"
        });

        await SendAsync(new DeleteBreweryCommand(breweryId));

        var beer = await FindAsync<Brewery>(breweryId);

        beer.Should().BeNull();
    }
}
