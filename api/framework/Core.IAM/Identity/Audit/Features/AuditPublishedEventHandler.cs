using Core.IAM.Identity.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using Core.IAM.Identity.Audit.Dtos;

namespace Core.IAM.Identity.Audit.Features;
public class AuditPublishedEventHandler(ILogger<AuditPublishedEventHandler> logger, IdentityDbContext context) : INotificationHandler<AuditPublishedEvent>
{
    public async Task Handle(AuditPublishedEvent notification, CancellationToken cancellationToken)
    {
        if (context == null) return;
        logger.LogInformation("received audit trails");
        try
        {
            await context.Set<AuditTrail>().AddRangeAsync(notification.Trails!, default);
            await context.SaveChangesAsync(default);
        }
        catch
        {
            logger.LogError("error while saving audit trail");
        }
        return;
    }
}
