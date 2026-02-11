namespace Catalog.API.Products.Queries.GetProducts;

public record GetProductsQueryResponse(IEnumerable<Product> Products);
public class GetProductsQueryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
            var response = result.Adapt<GetProductsQueryResponse>();
            return Results.Ok(response);
        })
            .WithTags("Products")
            .WithName("GetProducts")
            .Produces<GetProductsQueryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Gets all products")
            .WithDescription("Gets the full list of products in the catalog.");
    }
}
