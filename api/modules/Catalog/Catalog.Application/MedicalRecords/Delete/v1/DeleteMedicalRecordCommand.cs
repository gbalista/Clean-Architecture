using MediatR;

namespace Clinical.Application.MedicalRecords.Delete.v1;
public sealed record DeleteMedicalRecordCommand(
    Guid Id) : IRequest;
