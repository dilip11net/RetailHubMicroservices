

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string ImageFile, string Description, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsUpdated);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.").Length(2, 100).WithMessage("Product name must be between 2 and 100 characters.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("At least one category is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Product description is required.");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file path is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }

    internal class UpdateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            //Logger is moved to the pipeline, so we don't need to log here anymore.
            //logger.LogInformation("Handling UpdateProductCommand for ProductId: {ProductId}", command.Id);

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if(product == null)
            {
                //Logger is moved to the pipeline, so we don't need to log here anymore.
                //logger.LogWarning("Product with Id {ProductId} not found", command.Id);
                throw new ProductNotFoundException($"Product with Id {command.Id} not found");
            }

            // Perform the update logic here
            product.Name = command.Name;
            product.Category = command.Category;
            product.ImageFile = command.ImageFile;
            product.Description = command.Description;
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);


            return new UpdateProductResult(true);
        }
    }
}
