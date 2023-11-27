using MediatR;

namespace ShoppingList.DTO.Queries;
public record GetShoppingListsQuery(string? Name = null) : IRequest<IReadOnlyCollection<Models.ShoppingList>>;