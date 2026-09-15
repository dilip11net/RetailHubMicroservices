namespace Catalog.API.Products.DeleteProduct
{
    /// <summary>
    /// Represents the response for deleting a product.
    /// </summary>
    public record DeleteProductResponse(bool IsDeleted);
    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (string id, ISender sender) =>
            {
                var command = new DeleteProductCommand(Guid.Parse(id));
                var result = await sender.Send(command);

                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete a product by ID")
            .WithDescription("Deletes a product by its ID.");
        }
    }
}
