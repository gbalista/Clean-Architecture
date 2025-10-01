using Core.IAM.Auth.Policy;
using Clinical.Application.MedicalRecords.Update.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Clinical.Presentation.Endpoints.v1;
public static class UpdateMedicalRecordEndpoint
{
    internal static RouteHandlerBuilder MapMedicalRecordUpdateEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPut("/{id:guid}", async (Guid id, UpdateMedicalRecordCommand request, ISender mediator) =>
            {
                if (id != request.Id) return Results.BadRequest();
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(UpdateMedicalRecordEndpoint))
            .WithSummary("update a medical record")
            .WithDescription("update a medical record")
            .Produces<UpdateMedicalRecordResponse>()
            .RequirePermission("Permissions.MedicalRecords.Update")
            .MapToApiVersion(1);
    }
}
