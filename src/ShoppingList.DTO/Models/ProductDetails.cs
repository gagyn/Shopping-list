namespace ShoppingList.DTO.Models;
public record ProductDetails
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Amount { get; set; }
    public bool Completed { get; set; }

    public ProductDetails()
    {
    }

    public ProductDetails(int id, string name, string description, int amount, bool completed)
    {
        Id = id;
        Name = name;
        Description = description;
        Amount = amount;
        Completed = completed;
    }
}
