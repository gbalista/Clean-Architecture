using Finbuckle.MultiTenant.Abstractions;
using Clinical.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Core.DataAccess.Persistence;
using Core.DataAcess.Persistence;
using Core.Persistence.Tenant;
using Core.Persistence;

namespace Clinical.Persistence;

public sealed class ClinicalDbContext : ArcDbContext
{
    public ClinicalDbContext(IMultiTenantContextAccessor<ArcTenantInfo> multiTenantContextAccessor, DbContextOptions<ClinicalDbContext> options, IPublisher publisher, IOptions<DatabaseOptions> settings)
        : base(multiTenantContextAccessor, options, publisher, settings)
    {
    }

    public DbSet<MedicalRecord> MedicalRecords { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicalDbContext).Assembly);
        modelBuilder.HasDefaultSchema("clinical");
    }
}
