using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrder;

namespace Ordering.API.EndPoints
{
    //public record GetOrderRequest(PaginationRequest PaginationRequest);
    public record GetOrderResponse(PaginatedResult<OrderDto> Orders);
    public class GetOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters]PaginationRequest paginationRequest, ISender sender) =>
            {
                var result = await sender.Send(new GetOrderQuery(paginationRequest));
                var response = result.Adapt<GetOrderResponse>();
                return Results.Ok(response);
            })
                .WithName("GetOrder")
                .Produces<GetOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Gets orders with pagination")
                .WithDescription("Retrieves a paginated list of orders based on the specified pagination parameters.");
        }
    }
}
