using System.Collections.ObjectModel;
using Core.IAM.Identity.Audit.Dtos;
using MediatR;

namespace Core.IAM.Identity.Audit.Features;
public class AuditPublishedEvent : INotification
{
    public AuditPublishedEvent(Collection<AuditTrail>? trails)
    {
        Trails = trails;
    }
    public Collection<AuditTrail>? Trails { get; }
}
