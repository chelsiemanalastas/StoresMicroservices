namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    List<string> Categories,
    string Description,
    string ImageUrl,
    decimal Price) : ICommand<CreateProductCommandResult>;
public record CreateProductCommandResult(Guid Id);

internal class CreateProductCommandHandler(
        IDocumentSession documentSession,
        ILogger<CreateProductCommandHandler> logger
    ) : ICommandHandler<CreateProductCommand, CreateProductCommandResult>
{
    public async Task<CreateProductCommandResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling CreateProductCommand.Handle with {@Command}", command);
        var product = new Product
        {
            Name = command.Name,
            Categories = command.Categories,
            Description = command.Description,
            ImageUrl = command.ImageUrl,
            Price = command.Price,

        };

        documentSession.Store(product);
        await documentSession.SaveChangesAsync(cancellationToken);

        return new CreateProductCommandResult(product.Id);
    }
}
