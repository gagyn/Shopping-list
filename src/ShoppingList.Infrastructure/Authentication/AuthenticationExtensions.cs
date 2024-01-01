using Microsoft.AspNetCore.Components.Authorization;
using ShoppingList.DTO.Exceptions;
using System.Security.Claims;

namespace ShoppingList.Infrastructure.Authentication;
public static class AuthenticationExtensions
{
    public static async Task<string> GetUserName(this AuthenticationStateProvider stateProvider)
    {
        var state = await stateProvider.GetAuthenticationStateAsync();
        return state.GetUserName();
    }

    public static async Task<Guid> GetUserId(this AuthenticationStateProvider stateProvider)
    {
        var state = await stateProvider.GetAuthenticationStateAsync();
        return state.GetUserId();
    }

    public static string GetUserName(this AuthenticationState state)
        => state.User.FindFirst(ClaimTypes.Email)?.Value ?? throw new UnauthorizeException();

    public static Guid GetUserId(this AuthenticationState state)
    {
        var claim = state.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizeException();
        return Guid.Parse(claim);
    }
}