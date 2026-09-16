using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;


namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache)
        : IBasketRepository
    {
        public async Task<Cart> GetBasketAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(userId.ToString(), cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
                return JsonSerializer.Deserialize<Cart>(cachedBasket)!;

            var basket = await repository.GetBasketAsync(userId, cancellationToken);
            await cache.SetStringAsync(userId.ToString(), JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }
                
        public async Task<Cart> StoreBasketAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            await repository.StoreBasketAsync(cart, cancellationToken);
            await cache.SetStringAsync(cart.UserId.ToString(), JsonSerializer.Serialize(cart), cancellationToken);
            return cart;
        }

        public async Task<bool> DeleteBasketAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            await repository.DeleteBasketAsync(userId, cancellationToken);
            await cache.RemoveAsync(userId.ToString(), cancellationToken);
            return true;
        }
    }
}
