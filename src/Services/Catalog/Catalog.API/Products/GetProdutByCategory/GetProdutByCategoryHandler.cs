namespace Catalog.API.Products.GetProdutByCategory
{
    public record GetProdutByCategoryQuery(string Category) : IQuery<GetProdutByCategoryResult>;
    public record GetProdutByCategoryResult(IEnumerable<Product> Products);
    internal class GetProdutByCategoryQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProdutByCategoryQuery, GetProdutByCategoryResult>
    {
        public async Task<GetProdutByCategoryResult> Handle(GetProdutByCategoryQuery request, CancellationToken cancellationToken)
        {
            //Logger is moved to the pipeline, so we don't need to log here anymore.
            //logger.LogInformation("Handling GetProdutByCategoryQuery for category: {Category}", request.Category);
            var products = await session.Query<Product>()
                .Where(p => p.Category.Contains(request.Category))
                .ToListAsync(cancellationToken);

            return new GetProdutByCategoryResult(products);
        }
    }
}
