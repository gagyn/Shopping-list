namespace ShoppingList.DTO.Models;
public record RecipeShort
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; }

    public RecipeShort(int id, string name, string shortDescription)
    {
        Id = id;
        Name = name;
        ShortDescription = shortDescription;
    }
}
