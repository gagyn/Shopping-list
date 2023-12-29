using MediatR;
using ShoppingList.DTO.Models;

namespace ShoppingList.DTO.Queries;
public record GetFavoriteRecipesQuery : IRequest<IReadOnlyCollection<RecipeShort>>;