using MediatR;

namespace ShoppingList.DTO.Commands;
public record CreateRecipeCommand(string Name) : IRequest<int>;
