using ShoppingList.Domain.Base;
using ShoppingList.Domain.Exceptions;

namespace ShoppingList.Domain.Recipe;
public class RecipeEntity : BaseEntity
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ICollection<IngredientEntity> Ingredients { get; private set; }

    private RecipeEntity()
    {
    }

    private RecipeEntity(int id, string name, string description, string createdBy) : base(createdBy)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public static RecipeEntity Create(int id, string name, string description, string createdBy) => new(id, name, description, createdBy);

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
