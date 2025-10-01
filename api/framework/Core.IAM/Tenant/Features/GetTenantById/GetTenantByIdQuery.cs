using Core.IAM.Tenant.Dtos;
using MediatR;

namespace Core.IAM.Tenant.Features.GetTenantById;
public record GetTenantByIdQuery(string TenantId) : IRequest<TenantDetail>;
