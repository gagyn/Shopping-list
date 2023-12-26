using MediatR;

namespace ShoppingList.DTO.Commands;

public record UpdateProductCommand(
    int ShoppingListId,
    int ProductId,
    string Name,
    string Description,
    decimal Amount,
    bool IsCompleted,
    Models.Unit Unit) : IRequest;