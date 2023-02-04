using IteratesBrewManager.Application.Common.Exceptions;
using IteratesBrewManager.Application.Breweries.Commands.CreateBrewery;
using IteratesBrewManager.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace IteratesBrewManager.Application.IntegrationTests.Brewerys.Commands;

using static Testing;

public class CreateBreweryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateBreweryCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateBrewery()
    {
        var command = new CreateBreweryCommand
        {
            Name = "New Brewery"
        };

        var breweryId = await SendAsync(command);

        var brewery = await FindAsync<Brewery>(breweryId);

        brewery.Should().NotBeNull();
        brewery!.Name.Should().Be(command.Name);
        brewery.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        brewery.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
