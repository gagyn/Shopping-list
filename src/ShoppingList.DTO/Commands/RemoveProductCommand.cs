using MediatR;

namespace ShoppingList.DTO.Commands;
public record RemoveProductCommand(
    int ShoppingListId,
    int ProductId) : IRequest;