using MediatR;

namespace ShoppingList.DTO.Commands;
public record AddProductToShoppingListCommand(
    int ShoppingListId,
    string Name,
    string Description,
    decimal Amount,
    Models.Unit Unit) : IRequest<int>;