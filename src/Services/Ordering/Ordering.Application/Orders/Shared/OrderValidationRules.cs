using FluentValidation;

namespace Ordering.Application.Orders.Shared;

public static class OrderValidationRules
{
    public static IRuleBuilderOptions<T, Guid> ValidId<T>(this IRuleBuilder<T, Guid> ruleBuilder, string idType)
    {
        return ruleBuilder
            .NotNull()
            .WithMessage($"{idType} is required.");
    }

    public static IRuleBuilderOptions<T, string> ValidName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Order name is required.");
    }

    public static IRuleBuilderOptions<T, string> ValidAddress<T>(this IRuleBuilder<T, string> ruleBuilder, string addressType)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage($"{addressType} is required.");
    }

    public static IRuleBuilderOptions<T, IEnumerable<TElement>> ValidOrderItems<T, TElement>(this IRuleBuilder<T, IEnumerable<TElement>> ruleBuilder)
    {
        return ruleBuilder
            .NotNull()
            .Must(x => x.Any())
            .WithMessage("{PropertyName} cannot be empty.");
    }
}
