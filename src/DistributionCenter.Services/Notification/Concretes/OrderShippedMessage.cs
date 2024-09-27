namespace DistributionCenter.Services.Notification.Concretes;

using Dtos;
using Interfaces;

public class OrderShippedMessage(OrderDto orderDto) : IMessage
{
    public string Subject => "Transport Assigned to Your Order";

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
                        <h1>Transport Assigned</h1>
                    </div>
                    <div class='content'>
                        <p>A transport has been assigned to your order with ID <strong>{orderDto.OrderId}</strong>.</p>
                        <p>Your order is on its way! and will arrive on {orderDto.TimeToDeliver.Day}-{
                            orderDto.TimeToDeliver.Month}-{orderDto.TimeToDeliver.Year} at {
                                orderDto.TimeToDeliver.TimeOfDay.Hours}:{orderDto.TimeToDeliver.TimeOfDay.Minutes} minutes approximately.</p>
                    </div>
                    <div class='footer'>
                        <p>Thank you for your patience.</p>
                    </div>
                </div>
            </body>
            </html>";
    }
}
