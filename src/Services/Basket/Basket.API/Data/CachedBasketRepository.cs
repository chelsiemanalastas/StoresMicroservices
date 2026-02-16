using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache distributedCache) 
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string username, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await distributedCache.GetStringAsync(username, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket))
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;

        var basket = await basketRepository.GetBasketAsync(username, cancellationToken);
        await distributedCache.SetStringAsync(username, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await basketRepository.StoreBasketAsync(basket, cancellationToken);
        await distributedCache.SetStringAsync(basket.Username, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }
    public async Task<bool> DeleteBasketAsync(string username, CancellationToken cancellationToken = default)
    {
        await basketRepository.DeleteBasketAsync(username, cancellationToken);
        await distributedCache.RemoveAsync(username, cancellationToken);
        return true;
    }
}
