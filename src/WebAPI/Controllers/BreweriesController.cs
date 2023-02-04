using IteratesBrewManager.Application.Beers.Commands.CreateBeer;
using IteratesBrewManager.Application.Beers.Queries.GetBeers;
using IteratesBrewManager.Application.Breweries.Commands.CreateBrewery;
using IteratesBrewManager.Application.Breweries.Commands.DeleteBrewery;
using IteratesBrewManager.Application.Breweries.Commands.UpdateBrewery;
using IteratesBrewManager.Application.Breweries.Commands.DeleteBeer;
using IteratesBrewManager.Application.Breweries.Queries.GetBreweries;
using IteratesBrewManager.Application.Common.Models;
using IteratesBrewManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IteratesBrewManager.WebAPI.Controllers;

public class BreweriesController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<BreweryDto>> GetBreweries([FromQuery] GetBreweriesQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateBreweryCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateBreweryCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteBreweryCommand(id));

        return NoContent();
    }

    [HttpGet("{id}/Beers")]
    public async Task<IEnumerable<BeerDto>> GetBeers(int id)
    {
        return await Mediator.Send(new GetBeersByBreweryQuery(id));
    }

    [HttpPost("{id}/Beers")]
    public async Task<ActionResult<int>> AddBeer(int id, CreateBeerCommand command)
    {
        if (id != command.BrewerId)
        {
            return BadRequest();
        }

        return await Mediator.Send(command);
    }

    [HttpDelete("{id}/Beers/{beerId}")]
    public async Task<ActionResult> DeleteBeer(int id, int beerId)
    {
        await Mediator.Send(new DeleteBeerCommand(id, beerId));

        return NoContent();
    }
}
