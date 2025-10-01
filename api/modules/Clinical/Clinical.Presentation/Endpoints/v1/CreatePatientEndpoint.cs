using Core.IAM.Auth.Policy;
using Clinical.Application.Patients.Create.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Clinical.Presentation.Endpoints.v1;
public static class CreatePatientEndpoint
{
    internal static RouteHandlerBuilder MapPatientCreationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/", async (CreatePatientCommand request, ISender mediator) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(CreatePatientEndpoint))
            .WithSummary("creates a patient")
            .WithDescription("creates a patient")
            .Produces<CreatePatientResponse>()
            .RequirePermission("Permissions.Patients.Create")
            .MapToApiVersion(1);
    }
}
