using MediatR;

namespace ShoppingList.DTO.Queries;
public record GetShoppingListsQuery(string Name) : IRequest<IReadOnlyCollection<DTO.Models.ShoppingList>>;