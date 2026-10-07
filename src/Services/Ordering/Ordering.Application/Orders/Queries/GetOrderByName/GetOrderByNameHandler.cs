using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrderByName
{

    internal class GetOrderByNameHandler(IApplicationDBContext dbcontext) : IQueryHandler<GetOrderByNameQuery, GetOrderByNameResult>
    {
        public async Task<GetOrderByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
        {
            // Handle the query here
            // For example, you can retrieve the orders from the database
            var orders = await dbcontext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Value.Contains(query.Name))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);
           
            return new GetOrderByNameResult(orders.ToOrderListDto());
        }

        
    }
}
