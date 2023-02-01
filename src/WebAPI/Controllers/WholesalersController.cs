using IteratesBrewManager.Application.Wholesalers.Commands.AddSale;
using Microsoft.AspNetCore.Mvc;

namespace IteratesBrewManager.WebAPI.Controllers;

public class WholesalersController : ApiControllerBase
{

    [HttpPost("{id}/Sale")]
    public async Task<ActionResult<int>> AddSale(int id, AddSaleCommand command)
    {
        if (id != command.WholesalerId)
        {
            return BadRequest();
        }
        return await Mediator.Send(command);
    }

    [HttpPut("{id}/UpdateQuantity")]
    public async Task<ActionResult<int>> UpdateBeerQuantity(int id, UpdateBeerQuantityCommand command)
    {
        if (id != command.WholesalerId)
        {
            return BadRequest();
        }
        return await Mediator.Send(command);
    }
}
