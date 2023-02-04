using IteratesBrewManager.Application.Common.Exceptions;
using IteratesBrewManager.Application.Breweries.Commands.CreateBrewery;
using IteratesBrewManager.Application.Breweries.Commands.UpdateBrewery;
using IteratesBrewManager.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace IteratesBrewManager.Application.IntegrationTests.Brewerys.Commands;

using static Testing;

public class UpdateBreweryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidBreweryId()
    {
        var command = new UpdateBreweryCommand { Id = 99, Name = "New Brewery" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateBrewery()
    {
        var breweryId = await SendAsync(new CreateBreweryCommand
        {
            Name = "New Brewery"
        });

        var command = new UpdateBreweryCommand
        {
            Id = breweryId,
            Name = "beer two",
        };

        await SendAsync(command);

        var brewery = await FindAsync<Brewery>(breweryId);

        brewery.Should().NotBeNull();
        brewery!.Name.Should().Be(command.Name);
        brewery.LastModified.Should().NotBeNull();
        brewery.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
