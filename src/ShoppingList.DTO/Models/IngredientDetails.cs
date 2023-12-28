namespace ShoppingList.DTO.Models;
public record IngredientDetails
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Amount { get; set; }
    public Unit Unit { get; set; } = Unit.Quantity;

    public IngredientDetails()
    {
    }

    public IngredientDetails(int? id, string? name, string? description, decimal? amount, Unit unit)
    {
        Id = id;
        Name = name;
        Description = description;
        Amount = amount;
        Unit = unit;
    }
}
