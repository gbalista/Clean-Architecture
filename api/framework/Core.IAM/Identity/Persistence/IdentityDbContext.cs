using Core.IAM.Identity.RoleClaims;
using Core.IAM.Identity.Users;
using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Core.DataAccess.Persistence;
using Core.Persistence;
using Core.Persistence.Tenant;
using Core.IAM.Identity.Roles.Dtos;
using Core.IAM.Identity.Audit.Dtos;

namespace Core.IAM.Identity.Persistence;
public class IdentityDbContext : MultiTenantIdentityDbContext<ArcUser,
    ArcRole,
    string,
    IdentityUserClaim<string>,
    IdentityUserRole<string>,
    IdentityUserLogin<string>,
    ArcRoleClaim,
    IdentityUserToken<string>>
{
    private readonly DatabaseOptions _settings;
    private new ArcTenantInfo TenantInfo { get; set; }
    public IdentityDbContext(IMultiTenantContextAccessor<ArcTenantInfo> multiTenantContextAccessor, DbContextOptions<IdentityDbContext> options, IOptions<DatabaseOptions> settings) : base(multiTenantContextAccessor, options)
    {
        _settings = settings.Value;
        TenantInfo = multiTenantContextAccessor.MultiTenantContext.TenantInfo!;
    }

    public DbSet<AuditTrail> AuditTrails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!string.IsNullOrWhiteSpace(TenantInfo?.ConnectionString))
        {
            optionsBuilder.ConfigureDatabase(_settings.Provider, TenantInfo.ConnectionString);
        }
    }
}
