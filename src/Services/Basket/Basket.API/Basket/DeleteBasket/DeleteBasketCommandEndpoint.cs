namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketRequest(string Username) : ICommand<DeleteBasketResponse>;
public record DeleteBasketResponse(bool IsSuccess);
public class DeleteBasketCommandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{username}", async (string username, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(username));
            var response = result.Adapt<DeleteBasketResponse>();
            return Results.Ok(response);
        })
        .WithTags("Basket")
        .WithName("DeleteBasket")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Deletes a user's basket")
        .WithDescription("Deletes the user's basket and all items in it.");
    }
}
