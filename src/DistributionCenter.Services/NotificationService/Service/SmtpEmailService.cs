namespace DistributionCenter.Services.NotificationService.Service;

using System;
using System.Net;
using System.Net.Mail;
using Interfaces;
using RestSharp;

public class SmtpEmailService(ISmtpClient smtpClient) : IEmailService
{
    private readonly string _username = "loscuriosos63@gmail.com";
    private readonly string _password = "mtln ugqu qdhh gjsw";

    public RestResponse SendSimpleMessage(string mail, IMessage message)
    {
        if (message == null)
        {
            throw new ArgumentNullException(nameof(message), "Message cannot be null");
        }

        RestResponse response;

        try
        {
            using (MailMessage mailMessage = new())
            {
                mailMessage.From = new MailAddress(_username);
                mailMessage.To.Add(mail);
                mailMessage.Subject = "Hello";
                mailMessage.Body = message.GetMessageContent();
                mailMessage.IsBodyHtml = true;

                smtpClient.Credentials = new NetworkCredential(_username, _password);
                smtpClient.EnableSsl = true;

                smtpClient.Send(mailMessage);
            }

            response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "Message sent successfully"
            };
        }
        catch (SmtpException ex)
        {
            response = new RestResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = $"SMTP Error: {ex.Message}"
            };

            SendErrorNotification($"SMTP Error: {ex.Message}");
        }
        catch (FormatException ex)
        {
            response = new RestResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = $"Format Error: {ex.Message}"
            };

            SendErrorNotification($"Format Error: {ex.Message}");
        }

        return response;
    }

    public void SendErrorNotification(string errorMessage)
    {
        try
        {
            using (MailMessage mailMessage = new())
            {
                mailMessage.From = new MailAddress(_username);
                mailMessage.To.Add("curiosos-suppport@example.com");
                mailMessage.Subject = "Order Processing Error";
                mailMessage.Body = $"<p>An error occurred: {errorMessage}</p>";
                mailMessage.IsBodyHtml = true;

                smtpClient.Credentials = new NetworkCredential(_username, _password);
                smtpClient.EnableSsl = true;

                smtpClient.Send(mailMessage);
            }
        }
        catch (SmtpException ex)
        {
            Console.WriteLine("SMTP Error during error notification: " + ex.Message);
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Format Error during error notification: " + ex.Message);
        }
    }
}
