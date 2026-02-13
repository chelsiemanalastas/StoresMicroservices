namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductByCategoryQueryResult>;
public record GetProductByCategoryQueryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(
        IDocumentSession documentSession,
        ILogger<GetProductByCategoryQueryHandler> logger
    ) : IQueryHandler<GetProductsByCategoryQuery, GetProductByCategoryQueryResult>
{
    public async Task<GetProductByCategoryQueryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetProductByCategoryQuery for Category: {Category}", query.Category);

        var products = await documentSession.Query<Product>()
            .Where(p => p.Categories.Contains(query.Category))
            .ToListAsync();

        if (!products.Any())
            throw new NotFoundException(nameof(Product), nameof(Product.Categories), query.Category);

        return new GetProductByCategoryQueryResult(products);
    }
}
