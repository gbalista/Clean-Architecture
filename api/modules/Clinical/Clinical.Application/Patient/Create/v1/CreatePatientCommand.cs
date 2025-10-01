using System.ComponentModel;
using MediatR;

namespace Clinical.Application.Patients.Create.v1;
public sealed record CreatePatientCommand(
    [property: DefaultValue("Nome do Paciente")] string? Name,
    [property: DefaultValue("Descri��o do Paciente")] string? Description = null) : IRequest<CreatePatientResponse>;

