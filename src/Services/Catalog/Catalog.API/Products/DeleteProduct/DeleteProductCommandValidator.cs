namespace Catalog.API.Products.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductByIdCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).ValidId();
    }
}
