namespace Basket.API.Basket.StoreBasket;

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Shopping cart is required.");
        RuleFor(x => x.Cart.Username).NotNull().WithMessage("Username is required.");
    }
}
