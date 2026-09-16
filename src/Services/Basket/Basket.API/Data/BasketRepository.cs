using BuildingBlocks.Exceptions;
using Marten;

namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session)
        : IBasketRepository
    {
        public async Task<bool> DeleteBasketAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            //var basket = await session.LoadAsync<Cart>(userId, cancellationToken);
            //if (basket is null)
            //    throw new BasketNotFoundException($"Basket not found for user {userId}");
            var basket = await GetBasketOrThrowAsync(userId, cancellationToken);

            //session.Delete<Cart>(userId);
            //Since we already loaded the Cart, we can delete the loaded document directly. This also makes the intent very clear
            session.Delete(basket);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Cart> GetBasketAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            //var basket = await session.LoadAsync<Cart>(userId, cancellationToken);
            //return basket is null ? throw new BasketNotFoundException($"Basket not found for user {userId}") : basket;
            return await GetBasketOrThrowAsync(userId, cancellationToken);
        }

        public async Task<Cart> StoreBasketAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            session.Store(cart);
            await session.SaveChangesAsync(cancellationToken);
            return cart;
        }

        //Private Common Methods 
        private async Task<Cart> GetBasketOrThrowAsync(Guid userId,CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<Cart>(userId, cancellationToken);

            return basket
                ?? throw new BasketNotFoundException(
                    $"Basket not found for user {userId}");
        }
    }
}
