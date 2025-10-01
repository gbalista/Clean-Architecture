using Core.IAM.Tenant.Dtos;
using MediatR;

namespace Core.IAM.Tenant.Features.GetTenants;
public sealed class GetTenantsQuery : IRequest<List<TenantDetail>>;
