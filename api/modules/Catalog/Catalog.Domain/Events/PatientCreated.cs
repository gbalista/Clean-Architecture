using Core.Domain.Events;

namespace Clinical.Domain.Events;
public sealed record PatientCreated : DomainEvent
{
    public Patient? Patient { get; set; }
}
