using MediatR;

namespace ShoppingList.DTO.Commands.Users;
public record LockUnlockUserCommand(Guid UserId) : IRequest;