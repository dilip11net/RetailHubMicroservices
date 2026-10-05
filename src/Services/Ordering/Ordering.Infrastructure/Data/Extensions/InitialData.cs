

namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers => new List<Customer>
        {
            Customer.Create(CustomerId.Of(new Guid("7f3c9a21-6d84-4b17-a5e2-91c6f0d83b44")), "John Doe", "john.doe@example.com"),
             Customer.Create(CustomerId.Of(new Guid("c2e71b59-04af-48d3-9f26-7a1d5c8b6e30")), "Mack", "mack@example.com")
        };

        public static IEnumerable<Product> Products => new List<Product>
        {
            Product.Create(ProductId.Of(new Guid("a84f2c19-7d63-4e05-b91a-36c8f7d20e54")), "Sony Bravia 9", 10.99m,"Sony TV"),
            Product.Create(ProductId.Of(new Guid("5b17e9a3-2c46-4f81-a7d9-63e0c5b28f14")), "Samsung Neo", 19.99m,"Samsung TV"),
            Product.Create(ProductId.Of(new Guid("d93a6f71-8b25-4c09-95e3-17f2d64a0c58")), "LG OLED", 29.99m,"LG TV"),
            Product.Create(ProductId.Of(new Guid("2e6c8b45-91d7-43fa-b0a2-58c4e7391d06")), "Panasonic Viera", 39.99m,"Panasonic TV"),
        };

        public static IEnumerable<Order> OrderWithItems 
        {
            get
            {
               var address1 = Address.Of("John", "Doe", "john.doe@example.com", "123 Main St", "New York", "NY", "10001", "USA");
               var address2 = Address.Of("Mack", "Smith", "mack@example.com", "456 Elm St", "Los Angeles", "CA", "90001", "USA");

                var payment1 = Payment.Of("John Doe", "4111111111111111", "12/25", "123", 1);
                var payment2 = Payment.Of("Mack", "5555555555554444", "11/24", "456", 2);


                var Order1 = Order.Create(
                    OrderId.Of(Guid.NewGuid()), 
                    CustomerId.Of(new Guid("7f3c9a21-6d84-4b17-a5e2-91c6f0d83b44")), 
                    OrderName.Of("Order 1"),
                    shippingAddress: address1,
                    billingAddress: address1,
                    payment: payment1
                    );
             Order1.Add(ProductId.Of(new Guid("a84f2c19-7d63-4e05-b91a-36c8f7d20e54")), 2, 10.99m);
                Order1.Add(ProductId.Of(new Guid("5b17e9a3-2c46-4f81-a7d9-63e0c5b28f14")), 1, 19.99m);


                var order2 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("c2e71b59-04af-48d3-9f26-7a1d5c8b6e30")),
                    OrderName.Of("Order 2"),
                    shippingAddress: address2,
                    billingAddress: address2,
                    payment: payment2
                    );

                order2.Add(ProductId.Of(new Guid("a84f2c19-7d63-4e05-b91a-36c8f7d20e54")), 3, 10.99m);
                order2.Add(ProductId.Of(new Guid("2e6c8b45-91d7-43fa-b0a2-58c4e7391d06")), 2, 19.99m);

                return new List<Order> { Order1, order2 };



            }
        }
    }
}
