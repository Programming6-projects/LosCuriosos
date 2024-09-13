namespace DistributionCenter.Services.Notification.Interfaces;

using DistributionCenter.Commons.Results;

public interface IEmailService
{
    Result SendMessage(string mail, IMessage message);
    Result SendMessage(ICollection<string> mails, IMessage message);
}
