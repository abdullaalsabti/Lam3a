using System.Security.Claims;
using Lam3a.Utils;

namespace Lam3a.Services.Authentication;

public static class ClaimsService
{
    public static Guid? GetUserId(ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return null;

        if (Guid.TryParse(userIdClaim.Value, out var userId))
            return userId;

        return null;
    }

    private static Role? GetUserRole(ClaimsPrincipal user)
    {
        var roleClaim = user.FindFirst(ClaimTypes.Role);
        if (roleClaim == null)
            return null;

        if (!Enum.TryParse<Role>(roleClaim.Value, out var roleEnum))
            return null;

        return roleEnum;
    }

    public static bool IsClient(ClaimsPrincipal user)
    {
        return GetUserRole(user) == Role.Client;
    }

    public static bool IsProvider(ClaimsPrincipal user)
    {
        return GetUserRole(user) == Role.Provider;
    }

    public static bool IsAdmin(ClaimsPrincipal user)
    {
        return GetUserRole(user) == Role.Admin;
    }
}
