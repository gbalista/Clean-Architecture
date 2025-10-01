using Core.IAM.Auth.Policy;
using Clinical.Application.MedicalRecords.Delete.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Clinical.Presentation.Endpoints.v1;
public static class DeleteMedicalRecordEndpoint
{
    internal static RouteHandlerBuilder MapMedicalRecordDeleteEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete("/{id:guid}", async (Guid id, ISender mediator) =>
             {
                 await mediator.Send(new DeleteMedicalRecordCommand(id));
                 return Results.NoContent();
             })
            .WithName(nameof(DeleteMedicalRecordEndpoint))
            .WithSummary("deletes medical Record by id")
            .WithDescription("deletes medical record by id")
            .Produces(StatusCodes.Status204NoContent)
            .RequirePermission("Permissions.MedicalRecords.Delete")
            .MapToApiVersion(1);
    }
}
