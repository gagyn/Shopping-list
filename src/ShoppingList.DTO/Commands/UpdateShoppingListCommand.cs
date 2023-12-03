using MediatR;

namespace ShoppingList.DTO.Commands;

public record UpdateShoppingListCommand(
    int Id, string Name) : IRequest;