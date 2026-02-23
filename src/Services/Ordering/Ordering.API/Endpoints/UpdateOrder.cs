using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto Order);
public record UpdateOrderResult(bool IsSuccess);
public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateOrderCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateOrderResult>();
            return Results.Ok(response);
        })
        .WithTags("Order")
        .WithName("UpdateOrder")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Creates an order")
        .WithDescription("Creates an order with a list of items");
    }
}
