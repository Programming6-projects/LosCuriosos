namespace DistributionCenter.Services.Tests.NotificationService.Services;

using System.Net.Mail;
using DistributionCenter.Services.NotificationService.Concretes;
using DistributionCenter.Services.NotificationService.Interfaces;
using DistributionCenter.Services.NotificationService.Service;

public class SmtpEmailServiceTests
{
    [Fact]
    public void SendSimpleMessage_ShouldCallSendOnSmtpClient()
    {
        // Arrange
        Mock<ISmtpClient> mockSmtpClient = new();
        SmtpEmailService emailService = new(mockSmtpClient.Object);

        string recipientEmail = "recipient@example.com";
        HtmlMessage message = new("Order confirmation message");

        // Act
        _ = emailService.SendSimpleMessage(recipientEmail, message);

        // Assert
        mockSmtpClient.Verify(client => client.Send(It.IsAny<MailMessage>()), Times.Once);
    }

    [Fact]
    public void SendSimpleMessage_SmtpException_SendsErrorNotification()
    {
        // Arrange
        Mock<ISmtpClient> mockSmtpClient = new();
        _ = mockSmtpClient.Setup(client => client.Send(It.IsAny<MailMessage>()))
            .Throws(new SmtpException("SMTP error"));

        SmtpEmailService emailService = new(mockSmtpClient.Object);

        string recipientEmail = "recipient@example.com";
        HtmlMessage message = new("Order confirmation message");

        // Act
        _ = emailService.SendSimpleMessage(recipientEmail, message);

        // Assert
        mockSmtpClient.Verify(client =>
            client.Send(It.Is<MailMessage>(m => m.Subject == "Order Processing Error" &&
                                                m.Body.Contains("SMTP Error"))), Times.Once);
    }

    [Fact]
    public void SendErrorNotification_ShouldSendErrorNotificationEmail()
    {
        // Arrange
        Mock<ISmtpClient> mockSmtpClient = new();
        SmtpEmailService emailService = new(mockSmtpClient.Object);

        string errorMessage = "Test Error";

        // Redirect the console output to avoid printing
        TextWriter originalConsoleOut = Console.Out;
        using StringWriter consoleOutput = new();
        Console.SetOut(consoleOutput);

        // Act
        emailService.SendErrorNotification(errorMessage);

        // Reset console output
        Console.SetOut(originalConsoleOut);

        // Assert
        mockSmtpClient.Verify(client => client.Send(It.Is<MailMessage>(m =>
            m.Subject == "Order Processing Error" &&
            m.Body.Contains("Test Error", StringComparison.OrdinalIgnoreCase))), Times.Once);
    }
}
