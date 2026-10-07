
using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrder
{
    public record GetOrderQuery(PaginationRequest PaginationRequest) : IQuery<GetOrderResult>;


    public record GetOrderResult(PaginatedResult<OrderDto> Orders);

}
