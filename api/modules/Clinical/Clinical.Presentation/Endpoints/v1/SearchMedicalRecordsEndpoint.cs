using Core.DataAccess.Paging;
using Core.IAM.Auth.Policy;
using Clinical.Application.MedicalRecords.Get.v1;
using Clinical.Application.MedicalRecords.Search.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Clinical.Presentation.Endpoints.v1;

public static class SearchMedicalRecordsEndpoint
{
    internal static RouteHandlerBuilder MapGetMedicalRecordListEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/search", async (ISender mediator, [FromBody] SearchMedicalRecordsCommand command) =>
            {
                var response = await mediator.Send(command);
                return Results.Ok(response);
            })
            .WithName(nameof(SearchMedicalRecordsEndpoint))
            .WithSummary("Gets a list of medical records")
            .WithDescription("Gets a list of medical records with pagination and filtering support")
            .Produces<PagedList<MedicalRecordResponse>>()
            .RequirePermission("Permissions.MedicalRecords.View")
            .MapToApiVersion(1);
    }
}

