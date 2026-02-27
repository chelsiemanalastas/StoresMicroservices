using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(
        ISender sender, 
        ILogger<BasketCheckoutEventHandler> logger
    ) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        // TODO: fullfillment process
        logger.LogInformation("Integration event handled: {event}", context.Message.GetType().Name);

        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);
    }
    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.UserName,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            Status: Ordering.Domain.Enums.OrderStatus.Pending,
            OrderItems:
            [
                new OrderItemDto(orderId, new Guid("a1f3c2e4-5b6d-4c7a-8e9f-0a1b2c3d4e01"), 2, 1500.50M),
                new OrderItemDto(orderId, new Guid("b2e4d3c5-6a7b-4d8c-9f0e-1b2c3d4e5f02"), 3, 1200.75M)
            ]);

        return new CreateOrderCommand(orderDto);
    }
}
