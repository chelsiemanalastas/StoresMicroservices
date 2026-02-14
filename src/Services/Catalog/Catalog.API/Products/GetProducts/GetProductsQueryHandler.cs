namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(int? PageNumber, int? PageSize) : IQuery<GetProductsQueryResult>;
public record GetProductsQueryResult(IEnumerable<Product> Products);

public class GetProductsQueryHandler(
        IDocumentSession documentSession
    ) : IQueryHandler<GetProductsQuery, GetProductsQueryResult>
{
    public async Task<GetProductsQueryResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await documentSession.Query<Product>()
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetProductsQueryResult(products);
    }
}
