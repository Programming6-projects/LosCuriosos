namespace DistributionCenter.Services.Notification.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string reciever, IMessage message);
}
