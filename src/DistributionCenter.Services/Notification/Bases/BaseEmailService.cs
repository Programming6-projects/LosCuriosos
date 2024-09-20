namespace DistributionCenter.Services.Notification.Bases;

using System.Net;
using System.Net.Mail;
using Interfaces;

public abstract class BaseEmailService(string server, int port, string username, string password) : IEmailService
{
    public async Task SendEmailAsync(string reciever, IMessage message)
    {
        using SmtpClient client =
            new(server)
            {
                Port = port,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true,
            };

        using MailMessage mail =
            new()
            {
                From = new MailAddress(username),
                To = { reciever },
                Subject = message.Subject,
                Body = message.GetMessage(),
                IsBodyHtml = true,
            };

        await client.SendMailAsync(mail);
    }
}
