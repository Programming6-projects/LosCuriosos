namespace DistributionCenter.Services.NotificationService.Interfaces;

using System.Net;
using System.Net.Mail;

public interface ISmtpClient
{
    NetworkCredential Credentials { get; set; }
    bool EnableSsl { get; set; }
    void Send(MailMessage message);
}
