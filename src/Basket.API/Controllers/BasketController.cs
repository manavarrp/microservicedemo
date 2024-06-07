using AutoMapper;
using Basket.API.GrpcService;
using Basket.API.Models;
using Basket.API.Repositories;
using CoreApiResponse;
using EventBus.Message.Event;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly IPublishEndpoint _publishEndpoint;
        IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(string userName)
        {
            try
            {
                var basket = await _basketRepository.GetBasket(userName);
                return CustomResult("Consulta exitosa", basket);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart bastket)
        {
            try
            {
                //Comunication discount.grpc
                //calculate latest price
                foreach (var item in bastket.Items) 
                {
                    var coupon = await _discountGrpcService.GetDiscount(item.ProductId);
                    item.Price -= coupon.Amount;
                }
                var basket = await _basketRepository.UpdateBasket(bastket);
                return CustomResult("Carrito modificado", basket);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
            }

        [HttpDelete]
        [ProducesResponseType((typeof(void)),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            try
            {
                 await _basketRepository.DeleteBasket(userName);
                return CustomResult("Carrito Eliminado");

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }

        [HttpPost]
        [ProducesResponseType((typeof(void)), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout checkout)
        {
            var basket = await _basketRepository.GetBasket(checkout.UserName);
            if (basket == null)
            {
                return CustomResult("Carrito esta vacio", HttpStatusCode.BadRequest);
            }
            //send checkout event to RabbitMQ
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(checkout);
            eventMessage.TotalPrice = basket.TotalPrice;
            await _publishEndpoint.Publish(eventMessage);

            //remove basket
            await _basketRepository.DeleteBasket(checkout.UserName);
            return CustomResult("Orden ha sido puesta");
        }
    }
}
