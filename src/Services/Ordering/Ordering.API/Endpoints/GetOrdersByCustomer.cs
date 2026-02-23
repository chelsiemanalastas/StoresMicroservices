using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);
public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));
            var response = result.Adapt<GetOrdersByCustomerResponse>();
            return Results.Ok(response);
        })
        .WithTags("Order")
        .WithName("GetOrdersByCustomer")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Gets all orders by customer")
        .WithDescription("Gets all orders of a customer");
    }
}
