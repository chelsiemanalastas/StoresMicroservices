namespace Ordering.Domain.Models;

public class Product : Entity<ProductId>
{
    public string Name { get; private set; } = default!;
    public string Price { get; private set; } = default!;

    public static Product Create(ProductId id, string name, string price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(price);
        var product = new Product
        {
            Id = id,
            Name = name,
            Price = price
        };
        return product;
    }
}
