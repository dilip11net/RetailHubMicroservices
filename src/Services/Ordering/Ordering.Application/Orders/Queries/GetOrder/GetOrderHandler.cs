using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Orders.Queries.GetOrder
{
    public class GetOrderHandler(IApplicationDBContext dbcontext) : IQueryHandler<GetOrderQuery, GetOrderResult>
    {
        public async Task<GetOrderResult> Handle(GetOrderQuery query, CancellationToken cancellationToken)
        {
            // Implement the logic to retrieve the order based on the request parameters
            // For example, you might query a database or call an external service
            // Placeholder implementation
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbcontext.Orders.CountAsync(cancellationToken);

            var orders = await dbcontext.Orders
                .Include(o=>o.OrderItems)
                .OrderBy(o => o.OrderName.Value)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

   

            return new GetOrderResult(new BuildingBlocks.Pagination.PaginatedResult<OrderDto>(pageIndex, pageSize,totalCount, orders.ToOrderListDto()));
        }
    }
}
