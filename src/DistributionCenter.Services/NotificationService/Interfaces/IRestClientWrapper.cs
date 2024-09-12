namespace DistributionCenter.Services.NotificationService.Interfaces;

using RestSharp;

public interface IRestClientWrapper
{
    RestResponse Execute(RestRequest request);
}
