
namespace Shopping.Web.Services
{
    public interface IBasketService
    {
        [Get("/basket-service/basket/{userId}")]
        Task<GetBasketResponse> GetBasket(Guid userId);

        [Post("/basket-service/basket")]
        Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

        [Delete("/basket-service/basket/{userId}")]
        Task<DeleteBasketResponse> DeleteBasket(Guid userId);

        [Post("/basket-service/basket/checkout")]
        Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);
    }
}
