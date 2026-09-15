namespace Catalog.API.Products.GetProdutByCategory
{
    public record GetProdutByCategoryEndpointRequest(string Category) : IQuery<GetProdutByCategoryEndpointResponse>;
    public record GetProdutByCategoryEndpointResponse(IEnumerable<Product> Products);
    public class GetProdutByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProdutByCategoryQuery(category));
                var response = result.Adapt<GetProdutByCategoryEndpointResponse>();
                return Results.Ok(response);
            }).WithName("GetProdutByCategory")
              .Produces<GetProdutByCategoryEndpointResponse>(StatusCodes.Status200OK)
              .WithSummary("Retrieves products by category")
              .WithDescription("Retrieves a list of products that belong to a specific category.");
        }

    }
}
