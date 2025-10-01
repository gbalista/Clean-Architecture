using Core.Domain.Events;

namespace Clinical.Domain.Events;
public sealed record MedicalRecordUpdated : DomainEvent
{
    public MedicalRecord? MedicalRecord { get; set; }
}
