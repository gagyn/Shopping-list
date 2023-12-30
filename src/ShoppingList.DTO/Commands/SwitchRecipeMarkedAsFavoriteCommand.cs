using MediatR;

namespace ShoppingList.DTO.Commands;
public record SwitchRecipeMarkedAsFavoriteCommand(int RecipeId) : IRequest;