using Core.DataAccess.Paging;
using Core.IAM.Auth.Policy;
using Clinical.Application.Patients.Get.v1;
using Clinical.Application.Patients.Search.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Clinical.Presentation.Endpoints.v1;

public static class SearchPatientsEndpoint
{
    internal static RouteHandlerBuilder MapGetPatientListEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/search", async (ISender mediator, [FromBody] SearchPatientsCommand command) =>
            {
                var response = await mediator.Send(command);
                return Results.Ok(response);
            })
            .WithName(nameof(SearchPatientsEndpoint))
            .WithSummary("Gets a list of patients")
            .WithDescription("Gets a list of patients with pagination and filtering support")
            .Produces<PagedList<PatientResponse>>()
            .RequirePermission("Permissions.Patients.View")
            .MapToApiVersion(1);
    }
}
