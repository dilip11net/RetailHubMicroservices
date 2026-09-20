namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(Guid UserId, Cart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(Guid Success);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart is required.");
        }
    }
    public class StoreBasketCommandHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProtoService) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await DeductDiscount(command.Cart, cancellationToken);

            await basketRepository.StoreBasketAsync(command.Cart, cancellationToken);

            return new StoreBasketResult(command.Cart.UserId);
        }
    
    private async Task DeductDiscount(Cart cart, CancellationToken cancellationToken)
        {
            foreach(var item in cart.Items)
            {
                var coupon = await discountProtoService.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
        }
    }
}
