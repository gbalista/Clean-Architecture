using System.Security.Claims;
using Core.Exceptions;
using Core.IAM.Identity.Users.Abstractions;
using Core.IAM.Auth.Policy;

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Core.IAM.Identity.Users.Features.UpdateUser;
using Core.Auth.Identity;
using Core.IAM.Identity.Users.Services;

namespace Core.IAM.Identity.Users.Endpoints;
public static class UpdateUserEndpoint
{
    internal static RouteHandlerBuilder MapUpdateUserEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPut("/profile", (UpdateUserCommand request, ISender mediator, ClaimsPrincipal user, IUserService service) =>
        {
            if (user.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedException();
            }
            return service.UpdateAsync(request, userId);
        })
        .WithName(nameof(UpdateUserEndpoint))
        .WithSummary("update user profile")
        .RequirePermission("Permissions.Users.Update")
        .WithDescription("update user profile");
    }
}
