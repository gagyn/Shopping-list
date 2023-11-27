using MediatR;

namespace ShoppingList.DTO.Commands;
public record CreateShoppingListCommand(
    string Name) : IRequest<int>;