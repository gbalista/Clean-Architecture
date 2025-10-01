using Core.IAM.Auth.Policy;
using Clinical.Application.MedicalRecords.Create.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Clinical.Presentation.Endpoints.v1;
public static class CreateMedicalRecordEndpoint
{
    internal static RouteHandlerBuilder MapMedicalRecordCreationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/", async (CreateMedicalRecordCommand request, ISender mediator) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(CreateMedicalRecordEndpoint))
            .WithSummary("creates a medical record")
            .WithDescription("creates a medical record")
            .Produces<CreateMedicalRecordResponse>()
            .RequirePermission("Permissions.MedicalRecords.Create")
            .MapToApiVersion(1);
    }
}
