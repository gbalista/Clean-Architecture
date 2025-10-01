namespace Core.Mail;
public interface IMailService
{
    Task SendAsync(MailRequest request, CancellationToken ct);
}
