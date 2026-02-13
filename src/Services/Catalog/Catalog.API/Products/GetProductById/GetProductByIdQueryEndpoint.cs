namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQueryResponse(Product Product);
public class GetProductByIdQueryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProductByIdQueryResponse>();
            return Results.Ok(response);
        })
            .WithTags("Products")
            .WithName("GetProductsById")
            .Produces<GetProductByIdQuery>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Gets a product by id")
            .WithDescription("Gets a specific product using id");
    }
}
