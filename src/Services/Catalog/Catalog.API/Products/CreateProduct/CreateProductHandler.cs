

namespace Catalog.API.Products.CreateProduct
{


    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;

    /// <summary>
    /// Represents the response for creating a product.
    /// </summary>
    /// <param name="Id">The unique identifier for the product.</param>
    /// <param name="Name">The name of the product.</param>
    /// <param name="Category">The categories to which the product belongs.</param>
    /// <param name="Description">A description of the product.</param>
    /// <param name="ImageFile">The file path for the product's image.</param>
    /// <param name="Price">The price of the product.</param>
    //public record CreateProductResponse(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    /// <summary>
    /// Represents the result of creating a product.
    /// </summary>
    /// <param name="Id">The unique identifier for the product.</param>
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("At least one category is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Product description is required.");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file path is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }


    internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Product creation logic here (e.g., saving to a database)
            //Save the product to the database 


            // Validate the command using FluentValidation
            //var result = validator.Validate(command);
            //var error  = result.Errors.Select(e => e.ErrorMessage).ToList();

            //if (error.Any())
            //{
            //    throw new ValidationException(string.Join(";",error));
            //}
            logger.LogInformation("Creating a new product with name: {ProductName}", command.Name);
            var product = new Models.Product
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            //Save the product to the database (this is just a placeholder, implement your own logic)
            // Example: _context.Products.Add(product);
            // _context.SaveChanges();
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            // Return the result with the new product's ID
            return new CreateProductResult(product.Id);
        }
    }
}
