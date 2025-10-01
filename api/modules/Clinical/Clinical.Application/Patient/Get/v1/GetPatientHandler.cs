using Microsoft.Extensions.DependencyInjection;
using Clinical.Domain.Exceptions;
using Clinical.Domain;
using MediatR;
using Core.DataAcess.Persistence;
using Core.Caching;

namespace Clinical.Application.Patients.Get.v1;
public sealed class GetPatientHandler(
    [FromKeyedServices("clinical:patients")] IReadRepository<Patient> repository,
    ICacheService cache)
    : IRequestHandler<GetPatientRequest, PatientResponse>
{
    public async Task<PatientResponse> Handle(GetPatientRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var item = await cache.GetOrSetAsync(
            $"patient:{request.Id}",
            async () =>
            {
                var patientItem = await repository.GetByIdAsync(request.Id, cancellationToken);
                if (patientItem == null) throw new PatientNotFoundException(request.Id);
                return new PatientResponse(patientItem.Id, patientItem.Name, patientItem.Notes);
            },
            cancellationToken: cancellationToken);
        return item!;
    }
}
