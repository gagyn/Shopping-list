using MediatR;
using ShoppingList.DTO.Models.Users;

namespace ShoppingList.DTO.Commands.Users;
public record UpdateUserRoleCommand(Guid UserId, UserRole UserRole) : IRequest;