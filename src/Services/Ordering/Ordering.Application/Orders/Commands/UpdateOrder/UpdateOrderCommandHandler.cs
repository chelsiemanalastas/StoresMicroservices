namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler(
        IApplicationDbContext dbContext
    ) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(request.Order.Id);
        var order = await dbContext.Orders
            .FindAsync([orderId], cancellationToken);

        if (order is null)
            throw new OrderNotFoundException(nameof(UpdateOrderCommand), request.Order.OrderName, request.Order.Id.ToString());

        UpdateOrderDetails(order, request.Order);

        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }

    private void UpdateOrderDetails(Order order, OrderDto dto)
    {
        var shipping = dto.ShippingAddress;
        var billing = dto.BillingAddress;

        var shippingAddress = Address.Of(shipping.FirstName, shipping.LastName, shipping.EmailAddress, shipping.AddressLine, shipping.Country, shipping.State, shipping.ZipCode);
        var billingAddress = Address.Of(billing.FirstName, billing.LastName, billing.EmailAddress, billing.AddressLine, billing.Country, billing.State, billing.ZipCode);
        var payment = Payment.Of(dto.Payment.CardName, dto.Payment.CardNumber, dto.Payment.Expiration, dto.Payment.Cvv, dto.Payment.PaymentMethod);

        order.Update(
            OrderName.Of(dto.OrderName), 
            shippingAddress, 
            billingAddress, 
            payment, 
            dto.Status);
    }
}
