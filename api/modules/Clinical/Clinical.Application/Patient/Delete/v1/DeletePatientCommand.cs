using MediatR;

namespace Clinical.Application.Patients.Delete.v1;
public sealed record DeletePatientCommand(
    Guid Id) : IRequest;
