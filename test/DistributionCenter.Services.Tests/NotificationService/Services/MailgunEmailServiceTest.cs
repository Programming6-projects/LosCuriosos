namespace DistributionCenter.Services.Tests.NotificationService.Services;

using DistributionCenter.Services.NotificationService.Concretes;
using DistributionCenter.Services.NotificationService.Interfaces;
using DistributionCenter.Services.NotificationService.Service;
using Moq;
using RestSharp;
using Xunit;

public class MailgunEmailServiceTests
{
    [Fact]
    public void SendSimpleMessage_ReceivesResponse()
    {
        // Arrange
        Mock<IRestClientWrapper> mockClient = new();

        // Create a real RestResponse, manually configured with a 200 OK status code
        RestResponse mockResponse = new()
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            ResponseStatus = ResponseStatus.Completed
        };

        // Configure the mock client to return this RestResponse
        _ = mockClient.Setup(client => client.Execute(It.IsAny<RestRequest>()))
            .Returns(mockResponse);

        MailgunEmailService emailService = new(mockClient.Object);
        HtmlMessage message = HtmlMessage.CreateOrderConfirmation("123456");

        // Act
        RestResponse result = emailService.SendSimpleMessage("test@example.com", message);

        // Assert: Verify that the status code is 200 OK
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public void SendSimpleMessage_FailsWithInvalidEmail()
    {
        // Arrange
        Mock<IRestClientWrapper> mockClient = new();
        RestResponse mockResponse = new()
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest
        };

        _ = mockClient.Setup(client => client.Execute(It.IsAny<RestRequest>()))
            .Returns(mockResponse);

        MailgunEmailService emailService = new(mockClient.Object);
        HtmlMessage message = HtmlMessage.CreateOrderConfirmation("123456");

        // Act
        RestResponse result = emailService.SendSimpleMessage("invalid-email", message);

        // Assert
        Assert.False(result.IsSuccessful);
    }

    [Fact]
    public void SendSimpleMessage_ErrorNotification()
    {
        // Arrange
        Mock<IRestClientWrapper> mockClient = new();
        RestResponse mockErrorResponse = new()
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest
        };

        // Configure the client to return an error response first and then a successful response
        _ = mockClient.SetupSequence(client => client.Execute(It.IsAny<RestRequest>()))
            .Returns(mockErrorResponse) // First call returns an error response
            .Returns(new RestResponse { StatusCode = System.Net.HttpStatusCode.OK }); // Second call returns a success response

        MailgunEmailService emailService = new(mockClient.Object);
        HtmlMessage message = HtmlMessage.CreateOrderConfirmation("123456");

        // Act
        _ = emailService.SendSimpleMessage("test@example.com", message);

        // Assert
        // Verify that the client was called twice: once for the initial error and once for the error notification
        mockClient.Verify(client => client.Execute(It.Is<RestRequest>(req => req.Resource.Contains("messages") && req.Method == Method.Post)), Times.Exactly(2));
    }

    [Fact]
    public void SendSimpleMessage_CorrectRequestSent()
    {
        // Arrange
        Mock<IRestClientWrapper> mockClient = new();
        RestResponse mockResponse = new() { StatusCode = System.Net.HttpStatusCode.OK };
        _ = mockClient.Setup(client => client.Execute(It.IsAny<RestRequest>())).Returns(mockResponse);

        MailgunEmailService emailService = new(mockClient.Object);
        HtmlMessage message = new("<html><body>Test</body></html>");
        string mail = "test@example.com";

        // Act
        _ = emailService.SendSimpleMessage(mail, message);

        // Assert
        mockClient.Verify(client => client.Execute(It.Is<RestRequest>(req =>
            req.Parameters.Any(p => p.Name == "to" && p.Value != null && p.Value.ToString() == mail) &&
            req.Parameters.Any(p => p.Name == "html" && p.Value != null && p.Value.ToString() == message.GetMessageContent())
        )));
    }
}

