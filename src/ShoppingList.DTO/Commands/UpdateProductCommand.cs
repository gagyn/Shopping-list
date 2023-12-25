using MediatR;

namespace ShoppingList.DTO.Commands;

public record UpdateProductCommand(
    int ShoppingListId,
    int ProductId,
    string Name,
    string Description,
    int Amount,
    bool IsCompleted) : IRequest;