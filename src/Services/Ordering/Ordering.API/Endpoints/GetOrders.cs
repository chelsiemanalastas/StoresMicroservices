using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints;

public record GetOrdersReponse(PaginatedResult<OrderDto> Orders);
public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request,ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersQuery(request));
            var response = result.Adapt<GetOrdersReponse>();
            return Results.Ok(response);
        })
        .WithTags("Order")
        .WithName("GetOrders")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Gets all orders")
        .WithDescription("Gets all orders in the system");
    }
}
