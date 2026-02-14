namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdQueryResult>;
public record GetProductByIdQueryResult(Product Product);
internal class GetProductByIdQueryHandler(
        IDocumentSession documentSession
    ) : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResult>
{
    public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await documentSession.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
            throw new NotFoundException(nameof(Product), nameof(Product.Id), query.Id.ToString());

        return new GetProductByIdQueryResult(product);
    }
}
