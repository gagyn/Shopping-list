using MediatR;

namespace ShoppingList.DTO.Queries;

public record FindShoppingListQuery(int Id) : IRequest<Models.ShoppingListDetails>;