using MediatR;

namespace ShoppingList.DTO.Commands;
public record CreateShoppingListFromRecipeCommand(
    int RecipeId,
    int Name) : IRequest<int>;
