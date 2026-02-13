namespace Catalog.API.Products.Shared;

public static class ProductValidationRules
{
    public static IRuleBuilderOptions<T, Guid> ValidId<T>(this IRuleBuilder<T, Guid> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Product Id is required.");
    }

    public static IRuleBuilderOptions<T, string> ValidName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(250)
            .WithMessage("Product name must not exceed 250 characters.");
    }

    public static IRuleBuilderOptions<T, List<string>> ValidCategories<T>(
        this IRuleBuilder<T, List<string>> ruleBuilder)
    {
        return ruleBuilder
            .NotNull()
            .Must(categories => categories.Any())
            .WithMessage("At least one category is required.")
            .Must(categories => categories.All(c => !string.IsNullOrWhiteSpace(c)))
            .WithMessage("Category names must not be empty or whitespace.")
            .Must(categories => categories.All(c => c.Length <= 100))
            .WithMessage("Category names must not exceed 100 characters.");
    }

    public static IRuleBuilderOptions<T, string> ValidDescription<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Product description is required.")
            .MaximumLength(1000)
            .WithMessage("Product description must not exceed 1000 characters.");
    }

    public static IRuleBuilderOptions<T, decimal> ValidPrice<T>(
        this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThan(0)
            .WithMessage("Product price must be greater than zero.");
    }

}