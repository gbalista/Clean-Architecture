using Core.IAM.Tenant.Abstractions;
using Core.IAM.Tenant.Dtos;
using MediatR;

namespace Core.IAM.Tenant.Features.GetTenants;
public sealed class GetTenantsHandler(ITenantService service) : IRequestHandler<GetTenantsQuery, List<TenantDetail>>
{
    public Task<List<TenantDetail>> Handle(GetTenantsQuery request, CancellationToken cancellationToken)
    {
        return service.GetAllAsync();
    }
}
