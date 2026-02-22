namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;
public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName).ValidName();
        RuleFor(x => x.Order.CustomerId).ValidId("Customer");
        RuleFor(x => x.Order.OrderItems).ValidOrderItems();
    }
}
