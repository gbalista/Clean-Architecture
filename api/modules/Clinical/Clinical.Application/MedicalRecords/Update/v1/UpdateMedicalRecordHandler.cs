using Core.DataAcess.Persistence;
using Clinical.Domain;
using Clinical.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Clinical.Application.MedicalRecords.Update.v1;
public sealed class UpdateMedicalRecordHandler(
    ILogger<UpdateMedicalRecordHandler> logger,
    [FromKeyedServices("clinical:medicalRecords")] IRepository<MedicalRecord> repository)
    : IRequestHandler<UpdateMedicalRecordCommand, UpdateMedicalRecordResponse>
{
    public async Task<UpdateMedicalRecordResponse> Handle(UpdateMedicalRecordCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var medicalRecord = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = medicalRecord ?? throw new MedicalRecordNotFoundException(request.Id);
        var updatedMedicalRecord = medicalRecord.Update(request.Title, request.Description, DateTime.Now);
        await repository.UpdateAsync(updatedMedicalRecord, cancellationToken);
        logger.LogInformation("prontuário com id : {MedicalRecordId} atualizado.", medicalRecord.Id);
        return new UpdateMedicalRecordResponse(medicalRecord.Id);
    }
}
