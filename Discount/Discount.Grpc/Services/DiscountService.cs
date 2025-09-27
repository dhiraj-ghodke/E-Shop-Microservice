using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService
        (DiscountContext dbcontext,ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
     
        // Implement the service methods here
        // For example, you can implement GetDiscount, CreateDiscount, UpdateDiscount, and DeleteDiscount methods.
        // Each method should interact with the data layer to perform the required operations.

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbcontext
                .Coupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon is null)
            {
                coupon = new Coupon { ProductName = "No Discount", Amount = 0 , Description = "No Discount Desc"};    
            }
            logger.LogInformation("Discount is retrived for ProductName : {productName}, Amount : {amount}", coupon.ProductName,coupon.Amount);
            
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountsRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invaild request object."));

            dbcontext.Coupons.Add(coupon);
            await dbcontext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully created. ProductName : {productName}", coupon.ProductName);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;


        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invaild request object."));

            dbcontext.Coupons.Update(coupon);
            await dbcontext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully Updated. ProductName : {productName}", coupon.ProductName);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbcontext
                .Coupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProdcutName ={ request.ProductName} is not Found. "));

            dbcontext.Coupons.Remove(coupon);
            await dbcontext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully Deleted. ProductName : {productName}", request.ProductName);

            return new DeleteDiscountResponse { Success = true };
        }
    }
}
