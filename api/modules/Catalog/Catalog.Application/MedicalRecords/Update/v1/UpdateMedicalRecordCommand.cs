using MediatR;

namespace Clinical.Application.MedicalRecords.Update.v1;
public sealed record UpdateMedicalRecordCommand(
    Guid Id,
    string? Title,    
    string? Description = null,
    Guid? PatientId = null) : IRequest<UpdateMedicalRecordResponse>;
