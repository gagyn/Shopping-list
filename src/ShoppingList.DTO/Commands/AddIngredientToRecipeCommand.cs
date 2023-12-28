using MediatR;

namespace ShoppingList.DTO.Commands;
public record AddIngredientToRecipeCommand(
    int RecipeId,
    string Name,
    string Description,
    decimal Amount,
    Models.Unit Unit) : IRequest<int>;
