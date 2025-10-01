using Core.IAM.Auth.Policy;
using Clinical.Application.MedicalRecords.Get.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Clinical.Presentation.Endpoints.v1;
public static class GetMedicalRecordEndpoint
{
    internal static RouteHandlerBuilder MapGetMedicalRecordEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet("/{id:guid}", async (Guid id, ISender mediator) =>
            {
                var response = await mediator.Send(new GetMedicalRecordRequest(id));
                return Results.Ok(response);
            })
            .WithName(nameof(GetMedicalRecordEndpoint))
            .WithSummary("gets medical record by id")
            .WithDescription("gets medical record by id")
            .Produces<MedicalRecordResponse>()
            .RequirePermission("Permissions.MedicalRecords.View")
            .MapToApiVersion(1);
    }
}
