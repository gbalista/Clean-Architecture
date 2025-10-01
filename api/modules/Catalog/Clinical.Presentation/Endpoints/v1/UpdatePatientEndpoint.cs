using Core.IAM.Auth.Policy;
using Clinical.Application.Patients.Update.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Clinical.Presentation.Endpoints.v1;
public static class UpdatePatientEndpoint
{
    internal static RouteHandlerBuilder MapPatientUpdateEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPut("/{id:guid}", async (Guid id, UpdatePatientCommand request, ISender mediator) =>
            {
                if (id != request.Id) return Results.BadRequest();
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(UpdatePatientEndpoint))
            .WithSummary("update a patient")
            .WithDescription("update a patient")
            .Produces<UpdatePatientResponse>()
            .RequirePermission("Permissions.Patients.Update")
            .MapToApiVersion(1);
    }
}
