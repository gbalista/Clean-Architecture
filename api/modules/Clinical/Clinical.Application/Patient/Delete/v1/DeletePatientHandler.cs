using Clinical.Domain;
using Clinical.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Core.DataAcess.Persistence;

namespace Clinical.Application.Patients.Delete.v1;
public sealed class DeletePatientHandler(
    ILogger<DeletePatientHandler> logger,
    [FromKeyedServices("clinical:patients")] IRepository<Patient> repository)
    : IRequestHandler<DeletePatientCommand>
{
    public async Task Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var patient = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = patient ?? throw new PatientNotFoundException(request.Id);
        await repository.DeleteAsync(patient, cancellationToken);
        logger.LogInformation("Paciente com id : {PatientId} deletado", patient.Id);
    }
}
