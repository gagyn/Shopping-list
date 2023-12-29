using ShoppingList.DTO.Models;

namespace ShoppingList.Web.Client.Services;

public interface IRecipesClient
{
    Task<IReadOnlyCollection<RecipeShort>> GetRecipes(string? searchName = null);
}
