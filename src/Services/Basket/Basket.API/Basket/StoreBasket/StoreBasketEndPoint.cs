

using System.Reflection.Metadata;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketRequest(Cart Cart);
    public record StoreBasketResponse(Guid Success);
    public class StoreBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();

                var result = await sender.Send(command);

                var response =  result.Adapt<StoreBasketResponse>();

                return Results.Created($"/basket/{response.Success}", response);

            })
                .WithName("StoreBasket")
                .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithDescription("Store a basket for a user")
                .WithSummary("Store a basket for a user");
        }
    }
}
