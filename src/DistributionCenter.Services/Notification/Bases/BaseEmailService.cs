namespace DistributionCenter.Services.Notification.Bases;

using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Commons.Results;
using Interfaces;

public abstract class BaseEmailService(string server, int port, string username, string password) : IEmailService
{
    private readonly string _server = server;
    private readonly int _port = port;
    private readonly string _username = username;
    private readonly string _password = password;

    public Result SendMessage(string mail, IMessage message)
    {
        ArgumentNullException.ThrowIfNull(mail, nameof(mail));
        ArgumentNullException.ThrowIfNull(message, nameof(message));

        return SendMessage([mail], message);
    }

    public Result SendMessage(ICollection<string> mails, IMessage message)
    {
        ArgumentNullException.ThrowIfNull(mails, nameof(mails));
        ArgumentNullException.ThrowIfNull(message, nameof(message));

        try
        {
            using SmtpClient smtpClient = new(_server, _port);
            smtpClient.Credentials = new NetworkCredential(_username, _password);
            smtpClient.EnableSsl = true;

            using MailMessage mail = new();
            mail.From = new MailAddress(_username);

            foreach (string addressee in mails)
            {
                mail.To.Add(addressee);
            }

            mail.Subject = message.Subject;
            mail.Body = message.GetMessage();
            mail.IsBodyHtml = true;

            smtpClient.Send(mail);

            return Result.Ok();
        }
        catch (SmtpException ex)
        {
            return Error.Unexpected($"An error occurred while sending the email: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            return Error.Unexpected($"Invalid operation: {ex.Message}");
        }
    }
}
