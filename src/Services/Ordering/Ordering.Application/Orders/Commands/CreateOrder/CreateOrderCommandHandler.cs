using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(
        IApplicationDbContext dbContext
    ) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(request.Order);

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }

    private Order CreateNewOrder(OrderDto dto)
    {
        var shipping = dto.ShippingAddress;
        var billing = dto.BillingAddress;

        var shippingAddress = Address.Of(shipping.FirstName, shipping.LastName, shipping.EmailAddress, shipping.AddressLine, shipping.Country, shipping.State, shipping.ZipCode);
        var billingAddress = Address.Of(billing.FirstName, billing.LastName, billing.EmailAddress, billing.AddressLine, billing.Country, billing.State, billing.ZipCode);

        var newOrder = Order.Create(
            id: OrderId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(dto.CustomerId),
            orderName: OrderName.Of(dto.OrderName),
            shippingAddress: shippingAddress,
            billingAddress: billingAddress,
            payment: Payment.Of(dto.Payment.CardName, dto.Payment.CardNumber, dto.Payment.Expiration, dto.Payment.Cvv, dto.Payment.PaymentMethod),
            status: dto.Status
        );

        foreach (var item in dto.OrderItems)
        {
            newOrder.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
        }

        return newOrder;
    }
}
