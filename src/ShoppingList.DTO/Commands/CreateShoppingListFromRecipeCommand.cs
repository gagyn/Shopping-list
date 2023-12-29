using MediatR;

namespace ShoppingList.DTO.Commands;
public record CreateShoppingListFromRecipeCommand(
    int RecipeId,
    string Name) : IRequest<int>;
