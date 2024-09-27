namespace DistributionCenter.Services.Notification.Concretes;

using Dtos;
using Interfaces;

public class OrderCancelledMessage(OrderDto orderDto) : IMessage
{
    public string Subject => "Order Cancelled";

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
                        <h1>Order Cancelled</h1>
                    </div>
                    <div class='content'>
                        <p>Your order with ID <strong>{orderDto.OrderId}</strong> has been cancelled.</p>
                        <p>If you have any questions, please contact our support team at <a href='mailto:loscuriosos63@gmail.com'>loscuriosos63@gmail.com</a>.</p>
                    </div>
                    <div class='footer'>
                        <p>We apologize for any inconvenience caused.</p>
                    </div>
                </div>
            </body>
            </html>";
    }
}
