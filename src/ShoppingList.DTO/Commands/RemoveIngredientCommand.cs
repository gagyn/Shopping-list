using MediatR;

namespace ShoppingList.DTO.Commands;

public record RemoveIngredientCommand(
    int RecipeId,
    int IngredientId) : IRequest;