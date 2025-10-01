using Core.DataAcess.Persistence;
using Clinical.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Clinical.Application.MedicalRecords.Create.v1;
public sealed class CreateMedicalRecorHandler(
    ILogger<CreateMedicalRecorHandler> logger,
    [FromKeyedServices("clinical:medicalRecords")] IRepository<MedicalRecord> repository)
    : IRequestHandler<CreateMedicalRecordCommand, CreateMedicalRecordResponse>
{
    public async Task<CreateMedicalRecordResponse> Handle(CreateMedicalRecordCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var product = MedicalRecord.Create(request.Title!, request.Description, DateTime.Now, request.PatientId);
        await repository.AddAsync(product, cancellationToken);
        logger.LogInformation("prontuário criado {MedicalRecordId}", product.Id);
        return new CreateMedicalRecordResponse(product.Id);
    }
}
