namespace Catalog.API.Products.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product Id to update is required.");
        RuleFor(x => x.Name).ValidName();
        RuleFor(x => x.Categories).ValidCategories();
        RuleFor(x => x.Description).ValidDescription();
        RuleFor(x => x.Price).ValidPrice();
    }
}
