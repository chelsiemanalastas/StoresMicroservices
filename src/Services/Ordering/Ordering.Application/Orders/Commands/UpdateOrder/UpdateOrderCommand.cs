namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;
public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order.Id).ValidId("Order");
        RuleFor(x => x.Order.CustomerId).ValidId("Customer");
        RuleFor(x => x.Order.OrderName).ValidName();
        RuleFor(x => x.Order.OrderItems).ValidOrderItems();
    }
}