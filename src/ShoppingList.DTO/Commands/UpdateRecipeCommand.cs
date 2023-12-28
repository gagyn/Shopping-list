using MediatR;

namespace ShoppingList.DTO.Commands;

public record UpdateRecipeCommand(int Id, string Name, string Description) : IRequest<int>;