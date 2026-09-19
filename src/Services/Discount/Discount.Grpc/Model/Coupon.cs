namespace Discount.Grpc.Model
{
    public class Coupon
    {
        
        /// <summary>
        /// Coupon 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string ProductName { get; set; } = default!;

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; } = default!;

    }
}
