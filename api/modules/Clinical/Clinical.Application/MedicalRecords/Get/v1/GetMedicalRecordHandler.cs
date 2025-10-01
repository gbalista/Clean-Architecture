using Microsoft.Extensions.DependencyInjection;
using Clinical.Domain.Exceptions;
using Clinical.Domain;
using MediatR;
using Core.Caching;
using Core.DataAcess.Persistence;

namespace Clinical.Application.MedicalRecords.Get.v1;
public sealed class GetMedicalRecordHandler(
    [FromKeyedServices("clinical:medicalRecords")] IReadRepository<MedicalRecord> repository,
    ICacheService cache)
    : IRequestHandler<GetMedicalRecordRequest, MedicalRecordResponse>
{
    public async Task<MedicalRecordResponse> Handle(GetMedicalRecordRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await cache.GetOrSetAsync(
            $"medicalRecord:{request.Id}",
            async () =>
            {
                var spec = new GetMedicalRecordSpecs(request.Id);
                var medicalRecordItem = await repository.FirstOrDefaultAsync(spec, cancellationToken);
                if (medicalRecordItem == null) throw new MedicalRecordNotFoundException(request.Id);
                return medicalRecordItem;
            },
            cancellationToken: cancellationToken);
        return item!;
    }
}
