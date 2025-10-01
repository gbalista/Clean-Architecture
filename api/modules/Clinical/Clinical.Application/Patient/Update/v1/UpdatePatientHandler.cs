using Core.DataAcess.Persistence;
using Clinical.Domain;
using Clinical.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Clinical.Application.Patients.Update.v1;
public sealed class UpdatePatientHandler(
    ILogger<UpdatePatientHandler> logger,
    [FromKeyedServices("clinical:patients")] IRepository<Patient> repository)
    : IRequestHandler<UpdatePatientCommand, UpdatePatientResponse>
{
    public async Task<UpdatePatientResponse> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var patient = await repository.GetByIdAsync(request.Id, cancellationToken);
        _ = patient ?? throw new PatientNotFoundException(request.Id);
        var updatedBrand = patient.Update(request.Name, request.Notes);
        await repository.UpdateAsync(updatedBrand, cancellationToken);
        logger.LogInformation("Patient with id : {PatientId} updated.", patient.Id);
        return new UpdatePatientResponse(patient.Id);
    }
}
