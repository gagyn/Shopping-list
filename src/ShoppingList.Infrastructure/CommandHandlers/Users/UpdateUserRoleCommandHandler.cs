using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingList.Domain.Exceptions;
using ShoppingList.DTO.Commands.Users;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.CommandHandlers.Users;
public class UpdateUserRoleCommandHandler(
    UserManager<ApplicationUserEntity> userManager) : IRequestHandler<UpdateUserRoleCommand>
{
    public async Task Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString())
            ?? throw new EntityNotFoundException<ApplicationUserEntity>();

        var currentRole = (await userManager.GetRolesAsync(user)).First();
        await userManager.RemoveFromRoleAsync(user, currentRole);
        await userManager.AddToRoleAsync(user, request.UserRole.ToString());
    }
}
