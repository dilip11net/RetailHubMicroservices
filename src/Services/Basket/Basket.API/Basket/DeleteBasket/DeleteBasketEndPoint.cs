namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketRequest(Guid UserId);
    public record DeleteBasketResponse(bool Success);
    public class DeleteBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userId}", async (Guid userId, ISender sender) =>
            {
                var command = new DeleteBasketCommand(userId);

                var result = await sender.Send(command);

                var response = result.Adapt<DeleteBasketResponse>();

                return Results.Ok(response);
            })
                .WithName("DeleteBasket")
                .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithDescription("Delete a basket for a user")
                .WithSummary("Delete a basket for a user");
        }
    }
}
