namespace Core.IAM.Tenant.Dtos;
public class TenantDetail
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? ConnectionString { get; set; }
    public string AdminEmail { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTimeOffset ValidUpto { get; set; }
    public string? Issuer { get; set; }
}
