using MediatR;
using ShoppingList.DTO.Models;
using ShoppingList.DTO.Queries;
using ShoppingList.Web.Client.Services;

namespace ShoppingList.Web.Controllers;

public class RecipesService(IMediator mediator) : IRecipesClient
{
    public async Task<IReadOnlyCollection<RecipeShort>> GetRecipes(string? searchName)
        => await mediator.Send(new GetRecipesQuery(searchName));
}