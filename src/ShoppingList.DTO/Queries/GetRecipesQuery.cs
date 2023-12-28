using MediatR;
using ShoppingList.DTO.Models;

namespace ShoppingList.DTO.Queries;

public record GetRecipesQuery(string? Name = null) : IRequest<IReadOnlyCollection<RecipeShort>>;