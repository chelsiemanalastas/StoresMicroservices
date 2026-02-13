using Catalog.API.Products.Validators;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Categories,
    string Description,
    string ImageUrl,
    decimal Price) : ICommand<UpdateProductCommandResult>, IProductCommand;
public record UpdateProductCommandResult(bool IsSuccess);

internal class UpdateProductCommandHandler(
        IDocumentSession documentSession,
        ILogger<UpdateProductCommandHandler> logger
    ) : ICommandHandler<UpdateProductCommand, UpdateProductCommandResult>
{
    public async Task<UpdateProductCommandResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with command {@Command}", command);

        var product = await documentSession.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
            throw new NotFoundException(nameof(Product), nameof(Product.Id), command.Id.ToString());

        product.Name = command.Name;
        product.Categories = command.Categories;
        product.Description = command.Description;
        product.ImageUrl = command.ImageUrl;
        product.Price = command.Price;

        documentSession.Update(product);
        await documentSession.SaveChangesAsync(cancellationToken);

        return new UpdateProductCommandResult(true);
    }
}
