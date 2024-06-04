using Discount.Grpc.Protos;

namespace Basket.API.GrpcService
{
    public class DiscountGrpcService
    {
        public readonly DiscountProtoService.DiscountProtoServiceClient _discountService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountService)
        {
            _discountService = discountService;
        }
    }
}
