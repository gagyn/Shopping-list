using MediatR;

namespace ShoppingList.DTO.Commands;

public record RemoveRecipeCommand(
    int RecipeId) : IRequest;