using MassTransit;
using Microsoft.FeatureManagement;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(
        IPublishEndpoint publishEndpoint,
        IFeatureManager featureManager,
        ILogger<OrderCreatedEventHandler> logger
    ) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling OrderCreatedEvent for OrderId: {OrderId}, event name: {DomainEvent}", domainEvent.order.Id, domainEvent.GetType().Name);

        if (await featureManager.IsEnabledAsync("OrderFulfillment"))
        {
            var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();
            await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }

    }
}
