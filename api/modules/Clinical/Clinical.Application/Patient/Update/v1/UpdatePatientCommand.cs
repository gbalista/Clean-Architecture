using MediatR;

namespace Clinical.Application.Patients.Update.v1;
public sealed record UpdatePatientCommand(
    Guid Id,
    string? Name,
    string? Notes = null) : IRequest<UpdatePatientResponse>;
