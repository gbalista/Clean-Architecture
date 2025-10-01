using Core.Domain;
using Core.Domain.Contracts;
using Clinical.Domain;
using Clinical.Domain.Events;

namespace Clinical.Domain;

public class MedicalRecord : AuditableEntity, IAggregateRoot
{
    public string Title { get; private set; } = string.Empty;
    public string? Notes { get; private set; }
    public DateTime RecordDate { get; private set; }

    public Guid PatientId { get; private set; }
    public virtual Patient Patient { get; private set; } = default!;

    private MedicalRecord() { }

    private MedicalRecord(Guid id, string title, string? notes, DateTime recordDate, Guid patientId)
    {
        Id = id;
        Title = title;
        Notes = notes;
        RecordDate = recordDate;
        PatientId = patientId;

        QueueDomainEvent(new MedicalRecordCreated { MedicalRecord = this });
    }

    public static MedicalRecord Create(string title, string? notes, DateTime recordDate, Guid patientId)
    {
        return new MedicalRecord(Guid.NewGuid(), title, notes, recordDate, patientId);
    }

    public MedicalRecord Update(string? title, string? notes, DateTime? recordDate)
    {
        bool isUpdated = false;

        if (!string.IsNullOrWhiteSpace(title) && !string.Equals(Title, title, StringComparison.OrdinalIgnoreCase))
        {
            Title = title;
            isUpdated = true;
        }

        if (!string.Equals(Notes, notes, StringComparison.OrdinalIgnoreCase))
        {
            Notes = notes;
            isUpdated = true;
        }

        if (recordDate.HasValue && RecordDate != recordDate.Value)
        {
            RecordDate = recordDate.Value;
            isUpdated = true;
        }

        if (isUpdated)
        {
            QueueDomainEvent(new MedicalRecordUpdated { MedicalRecord = this });
        }

        return this;
    }
}

