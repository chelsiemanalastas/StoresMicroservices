namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderUpdatedEventHandler(
        ILogger<OrderUpdatedEventHandler> logger
    ) : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        // TODO: implementation
        logger.LogInformation("Handling OrderUpdatedEvent for OrderId: {OrderId}, event name: {DomainEvent}", notification.order.Id, notification.GetType().Name);
        return Task.CompletedTask;
    }
}
