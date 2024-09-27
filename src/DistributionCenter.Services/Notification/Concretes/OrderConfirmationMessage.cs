namespace DistributionCenter.Services.Notification.Concretes;

using Dtos;
using Interfaces;

public class OrderConfirmationMessage(OrderDto orderDto) : IMessage
{
    public string Subject => "Order Confirmation";

    public string GetMessage()
    {
        return $@"
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
                        <p>Your order with ID <strong>{orderDto.OrderId}</strong> has been received and is being processed.</p>
                    </div>
                    <div class='footer'>
                        <p>Thank you for choosing us!</p>
                    </div>
                </div>
            </body>
            </html>";
    }
}
