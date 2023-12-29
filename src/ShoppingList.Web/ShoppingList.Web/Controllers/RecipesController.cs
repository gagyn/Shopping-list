using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.DTO.Queries;

namespace ShoppingList.Web.Controllers;

[ApiController]
[Route("api/recipes")]
public class RecipesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRecipesAsync([FromQuery] string? searchName)
    {
        var recipes = await mediator.Send(new GetRecipesQuery(searchName));
        return Ok(recipes);
    }
}
