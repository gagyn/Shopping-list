using ShoppingList.Domain.Base;
using ShoppingList.Domain.Exceptions;

namespace ShoppingList.Domain.Recipe;
public class RecipeEntity : BaseEntity
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ICollection<IngredientEntity> Ingredients { get; private set; } = [];

    private RecipeEntity()
    {
    }

    private RecipeEntity(string name, string createdBy) : base(createdBy)
    {
        Name = name;
    }

    public static RecipeEntity Create(string name, string createdBy) => new(name, createdBy);

    public void Update(string name, string description, string modifiedBy)
    {
        Name = name;
        Description = description;
        SetModified(modifiedBy);
    }

    public void AddIngredient(IngredientEntity ingredientEntity, string modifiedBy)
    {
        Ingredients.Add(ingredientEntity);
        SetModified(modifiedBy);
    }

    public void RemoveIngredient(int ingredientId, string modifiedBy)
    {
        var ingredient = Ingredients.FirstOrDefault(x => x.Id == ingredientId)
            ?? throw new EntityNotFoundException<IngredientEntity>();

        Ingredients.Remove(ingredient);
        SetModified(modifiedBy);
    }
}
