

using Mapster;

namespace Basket.API.Basket.GetBasket
{
    /// <summary>
    /// Represents the request for getting a basket.
    /// </summary>
    public record GetBasketRequest(Guid UserId);

    /// <summary>
    /// Represents the response for getting a basket.
    /// </summary>
    public record GetBasketResponse(Cart cart);

    /// <summary>
    /// Represents the endpoint for getting a basket.
    /// </summary>
    public class GetBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userId:guid}", async (Guid userId, ISender sender) =>
             {
                 var query = new GetBasketQuery(userId);
                 var result = await sender.Send(query);
                 var response = result.Adapt<GetBasketResponse>();
                 return Results.Ok(response);
             })
             .WithName("GetBasket")
             .Produces<GetBasketResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .ProducesProblem(StatusCodes.Status404NotFound)
             .WithSummary("Get a basket by user ID")
             .WithDescription("Get a basket by user ID");
        }
    }
}
