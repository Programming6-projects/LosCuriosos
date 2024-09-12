namespace DistributionCenter.Services.NotificationService.Service;

using System.Text;
using Concretes;
using Interfaces;
using RestSharp;

public class MailgunEmailService(IRestClientWrapper client) : IEmailService
{
    private readonly string _domain = "sandbox843a364057074732963209c051944566.mailgun.org";
    private readonly string _apiKey = "cd178d76c14f2bd588ea6150ef763fcc-826eddfb-87d46213";

    public RestResponse SendSimpleMessage(string mail, IMessage message)
    {
        if (message == null)
        {
            throw new ArgumentNullException(nameof(message), "Message cannot be null");
        }

        RestRequest request = new("{domain}/messages", Method.Post);
        _ = request.AddUrlSegment("domain", _domain);
        _ = request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{_apiKey}")));
        _ = request.AddParameter("from", "mikyromesa100503@gmail.com");
        _ = request.AddParameter("to", mail);
        _ = request.AddParameter("subject", "Hello");
        _ = request.AddParameter("html", message.GetMessageContent());

        RestResponse response = client.Execute(request);

        if (!response.IsSuccessful)
        {
            string errorMessage = "An error occurred while sending the email.";
            HtmlMessage errorNotification = HtmlMessage.CreateErrorNotification(errorMessage);
            _ = client.Execute(new RestRequest("{domain}/messages", Method.Post)
                .AddUrlSegment("domain", _domain)
                .AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{_apiKey}")))
                .AddParameter("from", "mikyromesa100503@gmail.com")
                .AddParameter("to", "curiosos-suppport@example.com")
                .AddParameter("subject", "Order Processing Error")
                .AddParameter("html", errorNotification.GetMessageContent()));
        }

        return response;
    }
}
