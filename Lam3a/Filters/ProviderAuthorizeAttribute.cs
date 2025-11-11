using Lam3a.Data;
using Lam3a.Services.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Lam3a.Filters;

public class ProviderAuthorizeAttribute : Attribute, IAsyncActionFilter
{
    private readonly DataContextEf _context;

    public ProviderAuthorizeAttribute(DataContextEf context)
    {
        _context = context;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var user = context.HttpContext.User;

        var userId = ClaimsService.GetUserId(user);
        if (userId == null)
        {
            context.Result = new ObjectResult(new { message = "User ID not found or invalid." })
            {
                StatusCode = StatusCodes.Status401Unauthorized,
            };

            return;
        }

        if (!ClaimsService.IsProvider(user))
        {
            context.Result = new ObjectResult(
                new { message = "Only clients can access this endpoint." }
            )
            {
                StatusCode = StatusCodes.Status403Forbidden,
            };
            return;
        }

        var provider = await _context
            .Users.Include(u => u.Address)
            .FirstOrDefaultAsync(u => u.UserId == userId.Value);

        if (provider == null)
        {
            context.Result = new ObjectResult(new { message = "User not found or invalid." })
            {
                StatusCode = StatusCodes.Status404NotFound,
            };
            return;
        }

        // Store the user entity in HttpContext.Items so the controller can access it
        context.HttpContext.Items["Provider"] = provider;

        await next();
    }
}
