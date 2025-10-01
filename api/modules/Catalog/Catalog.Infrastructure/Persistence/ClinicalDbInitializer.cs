using Core.DataAcess.Persistence;
using Clinical.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Clinical.Persistence;
public sealed class ClinicalDbInitializer(
    ILogger<ClinicalDbInitializer> logger,
    ClinicalDbContext context) : IDbInitializer
{
    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        if ((await context.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
        {
            await context.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
            logger.LogInformation("[{Tenant}] applied database migrations for clinical module", context.TenantInfo!.Identifier);
        }
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        const string PatientName = "Maria da Silva";
        const string PatientDescription = "Paciente de teste para seed";

        const string Title = "Prontuário inicial";
        const string Notes = "Notas do primeiro prontuário";

        // Verifica se o paciente já existe
        var existingPatient = await context.Patients
            .FirstOrDefaultAsync(p => p.Name == PatientName, cancellationToken)
            .ConfigureAwait(false);

        Guid patientId;

        if (existingPatient is null)
        {
            var patient = Patient.Create(PatientName, PatientDescription);
            await context.Patients.AddAsync(patient, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            patientId = patient.Id;
        }
        else        
            patientId = existingPatient.Id;        

        // Verifica se já existe um prontuário com o mesmo título para esse paciente
        var existingRecord = await context.MedicalRecords
            .FirstOrDefaultAsync(m => m.Title == Title && m.PatientId == patientId, cancellationToken)
            .ConfigureAwait(false);

        if (existingRecord is null)
        {
            var record = MedicalRecord.Create(Title, Notes, DateTime.Now, patientId);
            await context.MedicalRecords.AddAsync(record, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            logger.LogInformation("[{Tenant}] Seeded default patient and medical record", context.TenantInfo!.Identifier);
        }
    }
}
