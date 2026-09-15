namespace Catalog.API.Products.GetProducts
{
    public record GetProductQuery() : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);

    public class GetProductQueryHandler(IDocumentSession session, ILogger<GetProductQueryHandler> logger)
        : IQueryHandler<GetProductQuery, GetProductResult>
    {
       public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductQuery with {0}", request);

            var products = session.Query<Product>().ToListAsync(cancellationToken);
            return await Task.FromResult(new GetProductResult(await products));
        }
    }
}
