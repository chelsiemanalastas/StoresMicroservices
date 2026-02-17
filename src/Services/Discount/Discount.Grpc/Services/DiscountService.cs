using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountDbContext dbContext, ILogger<DiscountService> logger) 
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
        if (coupon is null)
            coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No discount found" };

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
            
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));

        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Unable to update. Coupon [{request.Coupon}] was not found."));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Unable to delete. Coupon [{request.ProductName}] was not found."));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        return new DeleteDiscountResponse { Success = true };
    }
}
