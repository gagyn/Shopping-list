namespace ShoppingList.DTO.Models;
public record RecipeDetails
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool MarkedAsFavorite { get; set; }
    public IList<IngredientDetails> Ingredients { get; set; } = [];

    public RecipeDetails()
    {
    }

    public RecipeDetails(int id, string name, string description, bool markedAsFavorite, IList<IngredientDetails> ingredients)
    {
        Id = id;
        Name = name;
        Description = description;
        MarkedAsFavorite = markedAsFavorite;
        Ingredients = ingredients;
    }
}
