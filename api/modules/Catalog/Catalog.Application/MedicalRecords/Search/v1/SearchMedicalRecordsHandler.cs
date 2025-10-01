using Core.DataAccess.Paging;
using Core.DataAcess.Persistence;
using Clinical.Application.MedicalRecords.Get.v1;
using Clinical.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace Clinical.Application.MedicalRecords.Search.v1;
public sealed class SearchMedicalRecordsHandler(
    [FromKeyedServices("clinical:medicalRecords")] IReadRepository<MedicalRecord> repository)
    : IRequestHandler<SearchMedicalRecordsCommand, PagedList<MedicalRecordResponse>>
{
    public async Task<PagedList<MedicalRecordResponse>> Handle(SearchMedicalRecordsCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var spec = new SearchMedicalRecordsSpecs(request);

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedList<MedicalRecordResponse>(items, request!.PageNumber, request!.PageSize, totalCount);
    }
}

