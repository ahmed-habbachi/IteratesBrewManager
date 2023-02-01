using IteratesBrewManager.Application.Beers.Commands.CreateBeer;
using IteratesBrewManager.Application.Beers.Commands.DeleteBeer;
using IteratesBrewManager.Application.Beers.Commands.UpdateBeer;
using IteratesBrewManager.Application.Beers.Queries.GetBeers;
using IteratesBrewManager.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace IteratesBrewManager.WebAPI.Controllers;

public class BeersController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<BeerDto>> GetBeers([FromQuery] GetBeersQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateBeerCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateBeerCommand command)
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
        await Mediator.Send(new DeleteBeerCommand(id));

        return NoContent();
    }
}
