using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersByUserName;
using Ordering.Domain.Models;

namespace Ordering.Application.Mappings
{
    public class OrderMappingProfile :Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, GetOrdersByUserNameDto>().ReverseMap();

            CreateMap<Order, CreateOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }

    }
}
