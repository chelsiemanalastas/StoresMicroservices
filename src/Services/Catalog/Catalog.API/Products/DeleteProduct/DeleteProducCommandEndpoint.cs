namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommandRequest(Guid Id);
public record DeleteProductCommandResponse(bool IsSuccess);

public class DeleteProducCommandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductByIdCommand(id));
            var response = result.Adapt<DeleteProductCommandResponse>();
            return Results.Ok(response);
        })
            .WithTags("Products")
            .WithName("DeleteProduct")
            .Produces<DeleteProductCommandResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Deletes an existing product")
            .WithDescription("Permanently deletes an existing product with the provided id");
    }
}
