namespace DistributionCenter.Services.Notification.Interfaces;

public interface IMessage
{
    string Subject { get; }

    string GetMessage();
}
