namespace DistributionCenter.Services.NotificationService.Interfaces;

using RestResponse = RestSharp.RestResponse;

public interface IEmailService
{
    RestResponse SendSimpleMessage(string mail, IMessage message);
}
