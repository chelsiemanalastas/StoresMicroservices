using FluentValidation;

namespace Basket.API.Basket.Shared;

public static class BasketValidationRules
{
    public static IRuleBuilderOptions<T, Guid> ValidId<T>(this IRuleBuilder<T, Guid> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Product Id is required.");
    }
}
