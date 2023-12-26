namespace ShoppingList.DTO.Models;
public record ProductDetails
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Amount { get; set; }
    public bool Completed { get; set; }
    public Unit Unit { get; set; } = Unit.Quantity;

    public ProductDetails()
    {
    }

    public ProductDetails(int id, string name, string description, decimal amount, bool completed, Unit unit)
    {
        Id = id;
        Name = name;
        Description = description;
        Amount = amount;
        Completed = completed;
        Unit = unit;
    }
}
