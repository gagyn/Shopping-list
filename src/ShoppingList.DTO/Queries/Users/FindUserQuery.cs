using MediatR;
using ShoppingList.DTO.Models.Users;

namespace ShoppingList.DTO.Queries.Users;

public record FindUserQuery(Guid Id) : IRequest<UserDetails>;