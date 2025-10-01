using Core.IAM.Identity.Audit.Dtos;

namespace Core.IAM.Identity.Services;
public interface IAuditService
{
    Task<List<AuditTrail>> GetUserTrailsAsync(Guid userId);
}
