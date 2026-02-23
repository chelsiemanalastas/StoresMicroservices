using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrderByName;

public class GetOrderByNameQueryHandler(
        IApplicationDbContext dbContext
    ) : IQueryHandler<GetOrderByNameQuery, GetOrderByNameResult>

{
    public async Task<GetOrderByNameResult> Handle(GetOrderByNameQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.OrderName.Value.Contains(request.Name))
            .OrderBy(o => o.OrderName)
            .ToListAsync(cancellationToken);

        return new GetOrderByNameResult(orders.ToOrderDtoList());
    }

}
