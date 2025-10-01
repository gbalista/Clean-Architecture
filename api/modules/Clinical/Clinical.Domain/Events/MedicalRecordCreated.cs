using Core.Domain.Events;

namespace Clinical.Domain.Events;
public sealed record MedicalRecordCreated : DomainEvent
{
    public MedicalRecord? MedicalRecord { get; set; }
}
