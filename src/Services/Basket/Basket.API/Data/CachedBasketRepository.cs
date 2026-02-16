namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository basketRepository) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string username, CancellationToken cancellationToken = default)
    {
        return await basketRepository.GetBasketAsync(username, cancellationToken);
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        return await basketRepository.StoreBasketAsync(basket, cancellationToken);
    }
    public async Task<bool> DeleteBasketAsync(string username, CancellationToken cancellationToken = default)
    {
        return await basketRepository.DeleteBasketAsync(username, cancellationToken);
    }
}
