using Discount.Grpc.Data;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
            if (coupon == null)
            {
                coupon = new Model.Coupon
                {
                    ProductName = "No discount",
                    Description = "No discount available",
                    Amount = 0
                };
            }
            var loggerMessage = $"Discount retrieved for ProductName: {coupon.ProductName}, Amount: {coupon.Amount}";
            logger.LogInformation(loggerMessage);

            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {

            var coupon = request.Coupon.Adapt<Model.Coupon>();
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon cannot be null"));
            }
            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation($"Discount created for ProductName: {coupon.ProductName}, Amount: {coupon.Amount}");

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;



        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Model.Coupon>();
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon cannot be null"));
            }
            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation($"Discount updated for ProductName: {coupon.ProductName}, Amount: {coupon.Amount}");

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = dbContext.Coupons.FirstOrDefault(c => c.ProductName == request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon cannot be null or Not Found"));
            }
            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation($"Discount deleted for ProductName: {coupon.ProductName}, Amount: {coupon.Amount}");

            var response = new DeleteDiscountResponse
            {
                Success = true
            };
            return response;
        }
    }
}
