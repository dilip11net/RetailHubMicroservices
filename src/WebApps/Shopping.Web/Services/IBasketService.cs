
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

        public async Task<ShoppingCartModel> LoadUserBasket()
        {
            var userId = "a3f7b8c2-4d91-4e65-9a12-6f8c0d2e5b41"; // In a real application, you would get the user ID from the authenticated user context
            ShoppingCartModel basket;

            try
            {

                var basketResponse = await GetBasket(Guid.Parse(userId));
                basket = basketResponse.Cart;
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Error loading user basket for user {UserId}", userId);
                basket = new ShoppingCartModel
                {
                    UserId = Guid.Parse(userId),
                    Items = []
                };
            }

            return basket;
        }
    }
}
