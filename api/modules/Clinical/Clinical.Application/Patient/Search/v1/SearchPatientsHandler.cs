using Clinical.Application.Patients.Get.v1;
using Clinical.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Core.DataAcess.Persistence;
using Core.DataAccess.Paging;

namespace Clinical.Application.Patients.Search.v1;
public sealed class SearchPatientsHandler(
    [FromKeyedServices("clinical:patients")] IReadRepository<Patient> repository)
    : IRequestHandler<SearchPatientsCommand, PagedList<PatientResponse>>
{
    public async Task<PagedList<PatientResponse>> Handle(SearchPatientsCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new SearchPatientsSpecs(request);

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedList<PatientResponse>(items, request!.PageNumber, request!.PageSize, totalCount);
    }
}
