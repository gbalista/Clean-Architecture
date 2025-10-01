using Core.IAM.Identity.Users.Abstractions;
using Core.IAM.Auth.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Core.IAM.Identity.Users.Features.RegisterUser;
using Core.IAM.Identity.Users.Services;

namespace Core.IAM.Identity.Users.Endpoints;
public static class RegisterUserEndpoint
{
    internal static RouteHandlerBuilder MapRegisterUserEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/register", (RegisterUserCommand request,
            IUserService service,
            HttpContext context,
            CancellationToken cancellationToken) =>
        {
            var origin = $"{context.Request.Scheme}://{context.Request.Host.Value}{context.Request.PathBase.Value}";
            return service.RegisterAsync(request, origin, cancellationToken);
        })
        .WithName(nameof(RegisterUserEndpoint))
        .WithSummary("register user")
        .RequirePermission("Permissions.Users.Create")
        .WithDescription("register user");
    }
}
