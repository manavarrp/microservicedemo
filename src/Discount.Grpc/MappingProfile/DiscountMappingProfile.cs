using AutoMapper;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;

namespace Discount.Grpc.MappingProfile
{
    public class DiscountMappingProfile : Profile
    {
        public DiscountMappingProfile()
        {
            CreateMap<Coupon, CouponRequest>().ReverseMap();
        }
    }
}
