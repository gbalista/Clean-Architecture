using Core.IAM.Auth.Policy;
using Core.IAM.Identity.Roles.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Core.IAM.Identity.Roles.Endpoints;
public static class GetRolesEndpoint
{
    public static RouteHandlerBuilder MapGetRolesEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/", async (IRoleService roleService) =>
        {
            return await roleService.GetRolesAsync();
        })
        .WithName(nameof(GetRolesEndpoint))
        .WithSummary("Get a list of all roles")
        .RequirePermission("Permissions.Roles.View")
        .WithDescription("Retrieve a list of all roles available in the system.");
    }
}
