namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsDeleted);
    internal class DeleteProductHandler (IDocumentSession session, ILogger<DeleteProductHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting product with ID: {ProductId}", request.ProductId);

            var product = await session.LoadAsync<Product>(request.ProductId, cancellationToken );
            if (product == null)
            {
                logger.LogWarning("Product with ID: {ProductId} not found", request.ProductId);
                return await Task.FromResult(new DeleteProductResult(false));
            }

            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(new DeleteProductResult(true));

        }
    }
}
