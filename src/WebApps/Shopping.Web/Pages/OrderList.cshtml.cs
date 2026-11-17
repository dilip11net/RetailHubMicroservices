using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Web.Models.Ordering;

namespace Shopping.Web.Pages
{
    public class OrderListModel(IOrderingService orderingService, ILogger<OrderListModel> logger) : PageModel
    {
        public IEnumerable<OrderModel> Orders { get; set; } = default;
        public async Task<IActionResult> OnGetAsync()
        {
            var CustomerId = new Guid("96db3854-258a-4839-bbc3-7cbfee01ace7");
            var response = await orderingService.GetOrdersByCustomer(CustomerId);
            Orders = response.Orders;
            return Page();
        }
    }
}
