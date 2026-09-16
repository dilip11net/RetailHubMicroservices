using JasperFx;
using System.ComponentModel.DataAnnotations;

namespace Basket.API.Models
{
    public class Cart
    {
        [Identity]
        public Guid UserId { get; set; } 
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal TotalPrice { get; set; } = default!;

        public Cart(Guid userId)
        {
            UserId = userId;
        }
        public Cart()
        {
            
        }
    }

   
}
