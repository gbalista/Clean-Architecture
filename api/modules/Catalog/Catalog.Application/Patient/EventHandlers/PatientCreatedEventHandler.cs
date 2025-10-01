using Clinical.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinical.Application.Patients.EventHandlers;

public class PatientCreatedEventHandler(ILogger<PatientCreatedEventHandler> logger) : INotificationHandler<PatientCreated>
{
    public async Task Handle(PatientCreated notification,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("handling patient created domain event..");
        await Task.FromResult(notification);
        logger.LogInformation("finished handling patient created domain event..");
    }
}
