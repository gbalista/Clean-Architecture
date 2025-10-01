using System.ComponentModel;
using MediatR;

namespace Clinical.Application.MedicalRecords.Create.v1;
public sealed record CreateMedicalRecordCommand(
    [property: DefaultValue("Título do Prontuário")] string Title,
    [property: DefaultValue("Descrição do Prontuário")] string? Description,
    [property: DefaultValue("00000000-0000-0000-0000-000000000000")] Guid PatientId
) : IRequest<CreateMedicalRecordResponse>;
