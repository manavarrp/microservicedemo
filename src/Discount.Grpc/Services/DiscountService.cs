using AutoMapper;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        ICouponRepository _couponRepository;
        ILogger<DiscountService> _logger;
        IMapper _mapper;

        public DiscountService(ICouponRepository couponRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponRequest> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _couponRepository.GetDiscount(request.ProductId);
            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Descuento no encontrado"));
            }
            _logger.LogInformation("Descuento consultado para el ProductName:{productName}, Amount:{amount} ", coupon.ProductName, coupon.Amount);
            //return new CouponRequest { ProductId = coupon.ProductId, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount };
            return _mapper.Map<CouponRequest>(coupon);
        }

        public override async Task<CouponRequest> CreateDiscount(CouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            bool isSaved = await _couponRepository.CreateDiscount(coupon);
            if(!isSaved)
            {
                throw new RpcException(new Status(StatusCode.DataLoss, "Descuento no creado"));
            }
            _logger.LogInformation("Descuento creado.ProductName:{productName} ", coupon.ProductName);
            return _mapper.Map<CouponRequest>(coupon);

        }

        public override async Task<CouponRequest> UpdateDiscount(CouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            bool isSaved = await _couponRepository.UpdateDiscount(coupon);
            if (!isSaved)
            {
                throw new RpcException(new Status(StatusCode.DataLoss, "Descuento no actualizado"));
            }
            _logger.LogInformation("Descuento actualizado. ProductName:{productName} ", coupon.ProductName);
            return _mapper.Map<CouponRequest>(coupon);
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            bool isDeleted = await _couponRepository.DeleteDiscount(request.ProductId);
            if (!isDeleted)
            {
                _logger.LogInformation("Descuento no pudo ser eliminado.");
            }
            _logger.LogInformation("Descuento eliminado. ProductName:{ProductId} ", request.ProductId);
            return new DeleteDiscountResponse() { Success= isDeleted};
        }
    }
}
