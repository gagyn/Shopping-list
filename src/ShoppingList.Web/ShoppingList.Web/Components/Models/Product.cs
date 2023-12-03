namespace ShoppingList.Web.Components.Models;

public record ProductDto
{
    public ProductDto()
    {
    }

    public ProductDto(int id, string? name, string? description, int amount, bool completed)
    {
        Id = id;
        Name = name;
        Description = description;
        Amount = amount;
        Completed = completed;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Amount { get; set; }
    public bool Completed { get; set; }
}