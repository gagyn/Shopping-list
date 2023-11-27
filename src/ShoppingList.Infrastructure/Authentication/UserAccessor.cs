using Microsoft.AspNetCore.Http;
using ShoppingList.DTO.Exceptions;
using System.Security.Claims;

namespace ShoppingList.Infrastructure.Authentication;

public class UserAccessor(IHttpContextAccessor accessor) : IUserAccessor
{
    private readonly IHttpContextAccessor accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));

    public ClaimsPrincipal User => accessor.HttpContext.User;

    public string UserName => User.FindFirst(ClaimTypes.Email)?.Value ?? throw new UnauthorizeException();
    public Guid Id => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizeException());
}
