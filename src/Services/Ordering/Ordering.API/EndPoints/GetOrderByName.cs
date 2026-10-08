using Ordering.Application.Orders.Queries.GetOrderByName;

namespace Ordering.API.EndPoints
{
   // public record GetOrderByNameRequest(string Name);
    public record GetOrderByNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrderByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
            {
                var result = await sender.Send(new GetOrderByNameQuery(orderName));
                var response = result.Adapt<GetOrderByNameResponse>();
                return Results.Ok(response);
            })
                .WithName("GetOrderByName")
                .Produces<GetOrderByNameResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Gets an order by name")
                .WithDescription("Retrieves the details of an order with the specified name.");
        }
    }
}
