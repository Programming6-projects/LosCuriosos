namespace DistributionCenter.Services.NotificationService.Concretes;

using Interfaces;

public class HtmlMessage(string content) : IMessage
{
    public string GetMessageContent()
    {
        return content;
    }

    public static HtmlMessage CreateOrderConfirmation(string orderId)
    {
        return new HtmlMessage($@"
            <html>
            <head>
                <style>
                    body {{ font-family: Arial, sans-serif; margin: 20px; }}
                    .container {{ max-width: 600px; margin: auto; }}
                    .header {{ background: #f8f8f8; padding: 10px; text-align: center; }}
                    .content {{ margin: 20px 0; }}
                    .footer {{ background: #f8f8f8; padding: 10px; text-align: center; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h1>Order Confirmation</h1>
                    </div>
                    <div class='content'>
                        <p>Your order with ID <strong>{orderId}</strong> has been received and is being processed.</p>
                    </div>
                    <div class='footer'>
                        <p>Thank you for choosing us!</p>
                    </div>
                </div>
            </body>
            </html>");
    }

    public static HtmlMessage CreateErrorNotification(string errorMessage)
    {
        return new HtmlMessage($@"
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; margin: 20px; }}
                        .container {{ max-width: 600px; margin: auto; }}
                        .header {{ background: #f8f8f8; padding: 10px; text-align: center; }}
                        .content {{ margin: 20px 0; }}
                        .footer {{ background: #f8f8f8; padding: 10px; text-align: center; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>Order Processing Error</h1>
                        </div>
                        <div class='content'>
                            <p>We encountered an error while processing your order.</p>
                            <p><strong>Error:</strong> {errorMessage}</p>
                            <p>Please follow these steps to try again:</p>
                            <ul>
                                <li>Check your order details and try again.</li>
                                <li>If the problem persists, contact our support team at loscuriosos@gmail.com.</li>
                            </ul>
                        </div>
                        <div class='footer'>
                            <p>Thank you for your patience!</p>
                        </div>
                    </div>
                </body>
                </html>");
    }
}
