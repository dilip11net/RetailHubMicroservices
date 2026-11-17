using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class CartModel(IBasketService basketService, ILogger<CartModel> logger  ) 
        : PageModel
    {
        public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();
        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await basketService.LoadUserBasket();

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveFromCartAsync(Guid productId)
        { 
            logger.LogInformation("Remove from cart for product {ProductId} at {DT}", productId, DateTime.UtcNow.ToLongTimeString());
            var basket = await basketService.LoadUserBasket();
            var itemToRemove = basket.Items.FirstOrDefault(i => i.ProductId == productId);
            if (itemToRemove != null)
            {
                basket.Items.Remove(itemToRemove);
                await basketService.StoreBasket(new StoreBasketRequest(basket));
            }
            return RedirectToPage("Cart");
        }
    }
}
