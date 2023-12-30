using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingList.Domain.Exceptions;
using ShoppingList.DTO.Commands.Users;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.CommandHandlers.Users;
public class LockUnlockUserCommandHandler(
    UserManager<ApplicationUserEntity> userManager) : IRequestHandler<LockUnlockUserCommand>
{
    public async Task Handle(LockUnlockUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString())
            ?? throw new EntityNotFoundException<ApplicationUserEntity>();

        await userManager.SetLockoutEnabledAsync(user, enabled: !user.LockoutEnabled);
        await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
    }
}
