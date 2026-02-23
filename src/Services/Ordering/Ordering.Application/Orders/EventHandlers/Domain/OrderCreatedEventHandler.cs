namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(
        ILogger<OrderCreatedEventHandler> logger
    ) : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        // TODO: implementation
        logger.LogInformation("Handling OrderCreatedEvent for OrderId: {OrderId}, event name: {DomainEvent}", notification.order.Id, notification.GetType().Name);
        return Task.CompletedTask;
    }
}
