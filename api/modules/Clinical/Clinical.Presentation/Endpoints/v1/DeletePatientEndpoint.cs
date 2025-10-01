using Core.IAM.Auth.Policy;
using Clinical.Application.Patients.Delete.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Clinical.Presentation.Endpoints.v1;
public static class DeletePatientEndpoint
{
    internal static RouteHandlerBuilder MapPatientDeleteEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete("/{id:guid}", async (Guid id, ISender mediator) =>
             {
                 await mediator.Send(new DeletePatientCommand(id));
                 return Results.NoContent();
             })
            .WithName(nameof(DeletePatientEndpoint))
            .WithSummary("deletes patient by id")
            .WithDescription("deletes patient by id")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.Patients.Delete")
            .MapToApiVersion(1);
    }
}
