namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<Cart> GetBasketAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<Cart> StoreBasketAsync(Cart cart, CancellationToken cancellationToken = default);
        Task<bool> DeleteBasketAsync(Guid userId, CancellationToken cancellationToken = default);   
    }
}
