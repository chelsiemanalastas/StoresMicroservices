namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductByIdCommand(Guid Id) : ICommand<DeleteProductByIdCommandResult>;
public record DeleteProductByIdCommandResult(bool IsSuccess);
internal class DeleteProductCommandHandler(
        IDocumentSession documentSession
    ) : ICommandHandler<DeleteProductByIdCommand, DeleteProductByIdCommandResult>
{
    public async Task<DeleteProductByIdCommandResult> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        var product = await documentSession.LoadAsync<Product>(request.Id, cancellationToken);

        if (product is null)
            throw new NotFoundException(nameof(Product), nameof(Product.Id), request.Id.ToString());

        documentSession.Delete<Product>(request.Id);
        await documentSession.SaveChangesAsync(cancellationToken);

        return new DeleteProductByIdCommandResult(true);
    }
}
