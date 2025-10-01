using Finbuckle.MultiTenant.Abstractions;

namespace Core.Persistence.Tenant;
public interface IArcTenantInfo : ITenantInfo
{
    string? ConnectionString { get; set; }
}
