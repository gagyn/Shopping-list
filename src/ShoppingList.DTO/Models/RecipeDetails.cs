namespace ShoppingList.DTO.Models;
public record RecipeDetails
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IList<IngredientDetails> Ingredients { get; set; } = [];

    public RecipeDetails()
    {
    }

    public RecipeDetails(int id, string name, string description, IList<IngredientDetails> ingredients)
    {
        Id = id;
        Name = name;
        Description = description;
        Ingredients = ingredients;
    }
}
