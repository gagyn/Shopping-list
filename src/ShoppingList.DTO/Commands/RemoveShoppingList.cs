using MediatR;

namespace ShoppingList.DTO.Commands;

public record RemoveShoppingList(
    int ShoppingListId) : IRequest;