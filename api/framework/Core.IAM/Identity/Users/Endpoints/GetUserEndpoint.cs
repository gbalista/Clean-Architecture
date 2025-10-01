using Core.IAM.Identity.Users.Abstractions;
using Core.IAM.Auth.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Core.IAM.Identity.Users.Services;

namespace Core.IAM.Identity.Users.Endpoints;
public static class GetUserEndpoint
{
    internal static RouteHandlerBuilder MapGetUserEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/{id:guid}", (string id, IUserService service) =>
        {
            return service.GetAsync(id, CancellationToken.None);
        })
        .WithName(nameof(GetUserEndpoint))
        .WithSummary("Get user profile by ID")
        .RequirePermission("Permissions.Users.View")
        .WithDescription("Get another user's profile details by user ID.");
    }
}
