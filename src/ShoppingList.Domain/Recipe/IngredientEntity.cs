using ShoppingList.Domain.ShoppingList;

namespace ShoppingList.Domain.Recipe;

public class IngredientEntity
{
    public int Id { get; private set; }
    public int RecipeId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public UnitEntity Unit { get; private set; }

    private IngredientEntity()
    {
    }

    private IngredientEntity(string name, string description, decimal amount, UnitEntity unit, int recipeId)
    {
        Name = name;
        Description = description;
        Amount = amount;
        Unit = unit;
        RecipeId = recipeId;
    }

    public static IngredientEntity Create(string name, string description, decimal amount, UnitEntity unit, int recipeId)
        => new(name, description, amount, unit, recipeId);

    public void Update(string name, string description, decimal amount, UnitEntity unit)
    {
        Name = name;
        Description = description;
        Amount = amount;
        Unit = unit;
    }
}