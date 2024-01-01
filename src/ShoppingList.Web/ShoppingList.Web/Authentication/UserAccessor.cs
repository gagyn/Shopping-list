using ShoppingList.Domain.User;
using ShoppingList.DTO.Exceptions;
using ShoppingList.Infrastructure.Authentication;
using System.Security.Claims;

namespace ShoppingList.Web.Authentication;

public class UserAccessor(IHttpContextAccessor accessor) : IUserAccessor
{
    private readonly IHttpContextAccessor accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));

    public ClaimsPrincipal User => accessor.HttpContext?.User ?? throw new UnauthorizeException();

    public string UserName => User.FindFirst(ClaimTypes.Email)?.Value ?? throw new UnauthorizeException();
    public Guid Id => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizeException());

    public bool IsAdministrator() => User.IsInRole(UserRoleEntity.Administrator.ToString());
}