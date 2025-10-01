using Core.IAM.Auth.Policy;
using Clinical.Application.Patients.Get.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Clinical.Presentation.Endpoints.v1;
public static class GetPatientEndpoint
{
    internal static RouteHandlerBuilder MapGetPatientEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet("/{id:guid}", async (Guid id, ISender mediator) =>
            {
                var response = await mediator.Send(new GetPatientRequest(id));
                return Results.Ok(response);
            })
            .WithName(nameof(GetPatientEndpoint))
            .WithSummary("gets patient by id")
            .WithDescription("gets patient by id")
            .Produces<PatientResponse>()
            .RequirePermission("Permissions.Patients.View")
            .MapToApiVersion(1);
    }
}
