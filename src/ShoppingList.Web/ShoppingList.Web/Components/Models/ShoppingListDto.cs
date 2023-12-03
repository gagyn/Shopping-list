using ShoppingList.DTO.Models;

namespace ShoppingList.Web.Components.Models;

public class ShoppingListDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public IList<ProductDto> Products { get; set; } = [];

    public ShoppingListDto()
    {
    }

    public ShoppingListDto(ShoppingListDetails shoppingListDetails)
    {
        Id = shoppingListDetails.Id;
        Name = shoppingListDetails.Name;
        Products = shoppingListDetails.Products.Select(x => new ProductDto(x.Id, x.Name, x.Description, x.Amount, x.Completed)).ToList();
    }
}