namespace ShoppingList.DTO.Models;
public record ShoppingListDetails(int Id, string Name, IReadOnlyCollection<Product> Products);