using CoreApiResponse;
using Discount.API.Models;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DiscountController : BaseController
    {
        private readonly ICouponRepository _couponRepository;

        public DiscountController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetDiscount(string productId)
        {
            try
            {
                var coupon = await _couponRepository.GetDiscount(productId);
                return CustomResult("Consulta exitosa", coupon);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateDiscount([FromBody] Coupon coupon)
        {
            try
            {
                var isSaved = await _couponRepository.CreateDiscount(coupon);
                if (isSaved)
                {
                    return CustomResult("Cupón creado",coupon);
                }
                return CustomResult("Hubo un error al crear el cupón", coupon);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateDiscount([FromBody] Coupon coupon)
        {
            try
            {
                var isSaved = await _couponRepository.UpdateDiscount(coupon);
                if (isSaved)
                {
                    return CustomResult("Cupón actualizado", coupon);
                }
                return CustomResult("Hubo un error al actualizar el cupón", coupon);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteDiscount(string productId)
        {
            try
            {
                var isSaved = await _couponRepository.DeleteDiscount(productId);
                if (isSaved)
                {
                    return CustomResult("Cupón eliminado");
                }
                return CustomResult("Hubo un error al eliminar el cupón");

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }
    }
}
