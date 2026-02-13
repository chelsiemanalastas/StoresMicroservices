namespace Catalog.API.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(250)
            .WithMessage("Product name must not exceed 250 characters.");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Product description is required.")
            .MaximumLength(1000)
            .WithMessage("Product description must not exceed 1000 characters.");
        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .WithMessage("Product image URL is required.")
            .MaximumLength(500)
            .WithMessage("Product image URL must not exceed 500 characters.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("Product image URL must be a valid URL.");
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Product price must be greater than zero.");
        RuleFor(x => x.Categories)
            .NotEmpty()
            .WithMessage("At least one category is required.")
            .Must(categories => categories.All(c => !string.IsNullOrWhiteSpace(c)))
            .WithMessage("Category names must not be empty or whitespace.")
            .Must(categories => categories.All(c => c.Length <= 100))
            .WithMessage("Category names must not exceed 100 characters.");
    }
}
