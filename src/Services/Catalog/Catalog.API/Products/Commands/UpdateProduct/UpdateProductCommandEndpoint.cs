namespace Catalog.API.Products.Commands.UpdateProduct;

public record UpdateProductCommandRequest(
    Guid Id,
    string Name,
    List<string> Categories,
    string Description,
    string ImageUrl,
    decimal Price);
public record UpdateProductCommandResponse(bool IsSuccess);
public class UpdateProductCommandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductCommandRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommandRequest>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductCommandResponse>();
            return Results.Ok(response);
        })
            .WithTags("Products")
            .WithName("UpdateProduct")
            .Produces<UpdateProductCommandResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Updates an existing product")
            .WithDescription("Updates an existing product with the provided id");
    }
}
