using Ordering.Application.Orders.Queries.GetOrderByCustomer;

namespace Ordering.API.EndPoints
{
    //public record GetOrderByCustomerRequest(string CustomerId);
    public record GetOrderByCustomerResponse(IEnumerable<OrderDto> Orders);
    public class GetOrderByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
            {
                var result = await sender.Send(new GetOrderByCustomerQuery(customerId));
                var response = result.Adapt<GetOrderByCustomerResponse>();
                return Results.Ok(response);
            })
                .WithName("GetOrderByCustomer")
                .Produces<GetOrderByCustomerResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Gets orders by customer ID")
                .WithDescription("Retrieves all orders associated with the specified customer ID.");
        }
    }
}
