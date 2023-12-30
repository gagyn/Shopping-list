using MediatR;
using ShoppingList.DTO.Models.Users;

namespace ShoppingList.DTO.Queries.Users;
public record GetUsersQuery : IRequest<IReadOnlyCollection<UserDetails>>;