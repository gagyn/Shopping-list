using ShoppingList.DTO.Models;
using System.Net.Http.Json;

namespace ShoppingList.Web.Client.Services;

public class RecipesClient(HttpClient httpClient) : IRecipesClient
{
    public async Task<IReadOnlyCollection<RecipeShort>> GetRecipes(string? searchName)
    {
        var recipes = await httpClient.GetAsync("recipes");
        return await recipes.Content.ReadFromJsonAsync<IReadOnlyCollection<RecipeShort>>() ?? [];
    }
}