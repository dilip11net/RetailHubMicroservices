namespace Ordering.Domain.Models
{
    public class Product : Entity<ProductId>
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        public static Product Create(ProductId id, string name, decimal price, string description)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));
            ArgumentException.ThrowIfNullOrEmpty(description, nameof(description));
            return new Product
            {
                Id = id,
                Name = name,
                Price = price,
                Description = string.Empty
            };
        }
    }
}
