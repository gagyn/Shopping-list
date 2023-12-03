namespace ShoppingList.DTO.Models;
public class ShoppingListDetails
{
    public ShoppingListDetails(string name)
    {
        Name = name;
    }

    public ShoppingListDetails(int id, string name, IReadOnlyCollection<Product> products)
    {
        Id = id;
        Name = name;
        Products = products;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public IReadOnlyCollection<Product> Products { get; set; } = [];
}