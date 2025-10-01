using Core.DataAcess.Persistence;
using Clinical.Domain;
using Clinical.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Clinical.Application.MedicalRecords.Delete.v1;
public sealed class DeleteMedicalRecordHandler(
    ILogger<DeleteMedicalRecordHandler> logger,
    [FromKeyedServices("clinical:medicalRecords")] IRepository<MedicalRecord> repository)
    : IRequestHandler<DeleteMedicalRecordCommand>
{
    public async Task Handle(DeleteMedicalRecordCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var medicalRecord = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = medicalRecord ?? throw new MedicalRecordNotFoundException(request.Id);
        await repository.DeleteAsync(medicalRecord, cancellationToken);
        logger.LogInformation("prontuário com id : {MedicalRecordId} excluído", medicalRecord.Id);
    }
}
