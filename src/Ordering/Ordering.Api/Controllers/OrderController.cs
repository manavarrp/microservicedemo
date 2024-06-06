using CoreApiResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersByUserName;
using System.Net;

namespace Ordering.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : BaseController
    {
        IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetOrdersByUserNameDto>), (int)HttpStatusCode.OK)] 
        public async Task<IActionResult> GetOrders(string userName)
        {
            try
            {
               var orders = await _mediator.Send(new GetOrdersByUserNameQuery(userName));
                return CustomResult("Consulta existosa de ordenes",orders);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CrateOrder(CreateOrderCommand orderCommand)
        {
            try
            {
                var isOrderPlaced = await _mediator.Send(orderCommand);
                if (isOrderPlaced)
                {
                    return CustomResult("Orden creada con exito");

                }

                 return CustomResult("Ocurrio un error al crear la orden", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateOrder(UpdateOrderCommand orderCommand)
        {
            try
            {
                var isModified = await _mediator.Send(orderCommand);
                if (isModified)
                {
                    return CustomResult("Orden actualizada con exito");

                }

                return CustomResult("Ocurrio un error al actualizar la orden", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                var isDeleted = await _mediator.Send(new DeleteOrderCommand() { Id = orderId} );
                if (isDeleted)
                {
                    return CustomResult("Orden eliminada con exito");

                }

                return CustomResult("Ocurrio un error al eliminar la orden", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }
    }
}
