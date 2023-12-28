using MediatR;

namespace ShoppingList.DTO.Commands;

public record UpdateIngredientCommand(
    int RecipeId,
    int IngredientId,
    string Name,
    string Description,
    decimal Amount,
    Models.Unit Unit) : IRequest;