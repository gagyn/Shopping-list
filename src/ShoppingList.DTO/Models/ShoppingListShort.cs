namespace ShoppingList.DTO.Models;
public record ShoppingListShort
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ShoppingListShort(int id, string name)
    {
        Id = id;
        Name = name;
    }
}