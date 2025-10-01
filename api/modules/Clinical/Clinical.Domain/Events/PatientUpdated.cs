using Core.Domain.Events;

namespace Clinical.Domain.Events;
public sealed record PatientUpdated : DomainEvent
{
    public Patient? Patient { get; set; }
}
