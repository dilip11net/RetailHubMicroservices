using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if(await session.Query<Product>().AnyAsync())
            {
                return;
            }


            session.Store<Product>(GetPreConfiguredPRoducts());
            await session.SaveChangesAsync();
            

        }

        private static IEnumerable<Product> GetPreConfiguredPRoducts() => new List<Product>()
        {
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "IPhone X",
                Category = new List<string>() { "Smart Phone" },
                Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-1.png",
                Price = 950.00M
            },
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Samsung 10",
                Category = new List<string>() { "Smart Phone" },
                Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-2.png",
                Price = 840.00M
            },
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Huawei Plus",
                Category = new List<string>() { "White Appliances" },
                Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-3.png",
                Price = 650.00M
            },
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Xiaomi Mi 9",
                Category = new List<string>() { "White Appliances" },
                Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-4.png",
                Price = 470.00M
            },
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "HTC U11+ Plus",
                Category = new List<string>() { "Smart Phone" },
                Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-5.png",
                Price = 380.00M
            },
        };


    }
}
