namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommandRequest(
    string Name,
    List<string> Categories,
    string Description,
    string ImageUrl,
    decimal Price);

public record CreateProductCommandResponse(Guid Id);

public class CreateProductCommandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductCommandRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductCommandResponse>();
            return Results.Created($"/products/{response.Id}", response);
        })
            .WithTags("Products")
            .WithName("CreateProduct")
            .Produces<CreateProductCommandResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Creates a new product")
            .WithDescription("Creates a new product with the provided details.");
    }
}
