using Clinical.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Core.DataAcess.Persistence;

namespace Clinical.Application.Patients.Create.v1;
public sealed class CreatePatientHandler(
    ILogger<CreatePatientHandler> logger,
    [FromKeyedServices("clinical:patients")] IRepository<Patient> repository)
    : IRequestHandler<CreatePatientCommand, CreatePatientResponse>
{
    public async Task<CreatePatientResponse> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var patient = Patient.Create(request.Name!, request.Description);
        await repository.AddAsync(patient, cancellationToken);
        logger.LogInformation("patient created {PatientID}", patient.Id);
        return new CreatePatientResponse(patient.Id);
    }
}
