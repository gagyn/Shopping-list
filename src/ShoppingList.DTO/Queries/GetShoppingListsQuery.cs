using MediatR;
using ShoppingList.DTO.Models;

namespace ShoppingList.DTO.Queries;
public record GetShoppingListsQuery(string? Name = null) : IRequest<IReadOnlyCollection<ShoppingListShort>>;