namespace Catalog.API.Products.Queries.GetProductsByCategory;

public record GetProductByCategoryQueryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryQueryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/category/{category}", async (string category, ISender sender) =>
        {
            var result = await sender.Send(new GetProductsByCategoryQuery(category));
            var response = result.Adapt<GetProductByCategoryQueryResponse>();
            return Results.Ok(response);
        })
            .WithTags("Products")
            .WithName("GetProductsByCategory")
            .Produces<GetProductsByCategoryQuery>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Gets all products by category")
            .WithDescription("Gets the list of products filtered by provided category");
    }
}
