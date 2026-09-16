

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(Guid UserId) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool Success);

    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
        }
    }
    public class DeleteBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            // Implementation for handling the delete basket command

           await basketRepository.DeleteBasketAsync(command.UserId, cancellationToken);

            return new DeleteBasketResult(true);
        }
    }
}
