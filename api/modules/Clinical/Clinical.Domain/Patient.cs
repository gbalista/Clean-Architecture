using Core.Domain.Contracts;
using Clinical.Domain.Events;
using Core.Domain;

namespace Clinical.Domain;
public class Patient : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string? Notes { get; private set; }

    private Patient() { }

    private Patient(Guid id, string name, string? notes)
    {
        Id = id;
        Name = name;
        Notes = notes;
        QueueDomainEvent(new PatientCreated { Patient = this });
    }

    public static Patient Create(string name, string? notes)
    {
        return new Patient(Guid.NewGuid(), name, notes);
    }

    public Patient Update(string? name, string? notes)
    {
        bool isUpdated = false;

        if (!string.IsNullOrWhiteSpace(name) && !string.Equals(Name, name, StringComparison.OrdinalIgnoreCase))
        {
            Name = name;
            isUpdated = true;
        }

        if (!string.Equals(Notes, notes, StringComparison.OrdinalIgnoreCase))
        {
            Notes = notes;
            isUpdated = true;
        }

        if (isUpdated)
        {
            QueueDomainEvent(new PatientUpdated { Patient = this });
        }

        return this;
    }
}

