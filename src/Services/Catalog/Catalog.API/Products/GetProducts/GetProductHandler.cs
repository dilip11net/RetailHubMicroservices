
namespace Catalog.API.Products.GetProducts
{
    public record GetProductQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);

    public class GetProductQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductQuery, GetProductResult>
    {
       public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            //Logger is moved to the pipeline, so we don't need to log here anymore.
            // logger.LogInformation("Handling GetProductQuery with {0}", request);

            var products = await session.Query<Product>().ToPagedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);
            return await Task.FromResult(new GetProductResult(products));
        }
    }
}
