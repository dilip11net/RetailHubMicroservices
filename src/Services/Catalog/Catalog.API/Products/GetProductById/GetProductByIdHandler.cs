namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductByIdQuery with {0}", query);

            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
            
            if(product == null)
            {
                logger.LogWarning("Product with Id {0} not found", query.Id);
                throw new ProductNotFoundException($"Product with Id {query.Id} not found");
            }
            return new GetProductByIdResult(product);
        }
    }
}
