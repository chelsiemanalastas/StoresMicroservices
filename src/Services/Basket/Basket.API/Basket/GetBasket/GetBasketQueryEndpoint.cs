namespace Basket.API.Basket.GetBasket;

public record GetBasketRequest(string Username);
public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketQueryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{username}", async (string username, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(username));
            var response = result.Adapt<GetBasketResponse>();
            return Results.Ok(response);
        })
            .WithTags("Basket")
            .WithName("GetBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Gets a user's basket")
            .WithDescription("Gets all items in the user's basket.");
    }
}
