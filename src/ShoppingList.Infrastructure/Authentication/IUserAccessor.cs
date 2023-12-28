using System.Security.Claims;

namespace ShoppingList.Infrastructure.Authentication;

public interface IUserAccessor
{
    ClaimsPrincipal User { get; }
    string UserName { get; }
    Guid Id { get; }
    bool IsAdministrator();
}
