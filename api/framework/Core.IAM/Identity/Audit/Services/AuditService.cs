using Core.IAM.Identity.Persistence;
using Microsoft.EntityFrameworkCore;
using Core.IAM.Identity.Audit.Dtos;
using Core.IAM.Identity.Services;

namespace Core.IAM.Identity.Audit.Services;
public class AuditService(IdentityDbContext context) : IAuditService
{
    public async Task<List<AuditTrail>> GetUserTrailsAsync(Guid userId)
    {
        var trails = await context.AuditTrails
           .Where(a => a.UserId == userId)
           .OrderByDescending(a => a.DateTime)
           .Take(250)
           .ToListAsync();
        return trails;
    }
}
