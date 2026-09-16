

using Basket.API.Data;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(Guid UserId) : IQuery<GetBasketResult>;
    public record GetBasketResult(Cart cart);
    public class GetBasketQueryHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasketAsync(query.UserId);
            return new GetBasketResult(basket);
        }
    }
}
