namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string Username);
public class StoreBasketCommandHandler(
        IBasketRepository basketRepository
    ) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;
        await basketRepository.StoreBasketAsync(cart, cancellationToken);

        return new StoreBasketResult(command.Cart.Username);
    }
}
