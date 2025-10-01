using MediatR;

namespace Core.IAM.Tenant.Features.CreateTenant;
public sealed record CreateTenantCommand(string Id,
    string Name,
    string? ConnectionString,
    string AdminEmail,
    string? Issuer) : IRequest<CreateTenantResponse>;
