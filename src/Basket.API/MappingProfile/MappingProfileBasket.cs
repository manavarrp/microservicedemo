using AutoMapper;
using Basket.API.Models;
using EventBus.Message.Event;

namespace Basket.API.MappingProfile
{
    public class MappingProfileBasket : Profile
    {
        public MappingProfileBasket()
        {
            CreateMap<BasketCheckoutEvent, BasketCheckout>().ReverseMap();
        }
    }
}
