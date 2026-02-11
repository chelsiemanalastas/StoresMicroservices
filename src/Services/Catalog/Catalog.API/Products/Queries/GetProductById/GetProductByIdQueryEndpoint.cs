using Catalog.API.Products.Queries.GetProducts;

namespace Catalog.API.Products.Queries.GetProductById;

public record GetProductByIdResponse(Product Product);
public class GetProductByIdQueryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
        })
            .WithTags("Products")
            .WithName("GetProductsById")
            .Produces<GetProductsQueryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Gets a product by id")
            .WithDescription("Gets a specific product using id");
    }
}
