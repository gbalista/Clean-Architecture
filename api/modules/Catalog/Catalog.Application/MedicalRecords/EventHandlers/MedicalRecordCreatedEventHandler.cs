using Clinical.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinical.Application.MedicalRecords.EventHandlers;

public class MedicalRecordCreatedEventHandler(ILogger<MedicalRecordCreatedEventHandler> logger) : INotificationHandler<MedicalRecordCreated>
{
    public async Task Handle(MedicalRecordCreated notification,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("handling medical Record created domain event..");
        await Task.FromResult(notification);
        logger.LogInformation("finished handling medical Record created domain event..");
    }
}

