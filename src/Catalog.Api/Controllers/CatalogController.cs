using Catalog.Api.Interfaces.Manager;
using Catalog.Api.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net;

namespace Catalog.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : BaseController
    {
        private readonly IProductManager _productManager;

        public CatalogController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        [ResponseCache(Duration =30)]
        public IActionResult GetProducts() 
        {
            try
            {
                var products = _productManager.GetAll();
                return CustomResult("Consulta exitosa" , products);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 30)]
        public IActionResult GetById(string id)
        {
            try
            {
                var product = _productManager.GetById(id);
                return CustomResult("Consulta exitosa", product);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 30)]
        public IActionResult GetByCategory(string category)
        {
            try
            {
                var products = _productManager.GetByCategory(category);
                return CustomResult("Consulta exitosa", products);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            try
            {
                product.Id = ObjectId.GenerateNewId().ToString();
                bool isSaved = _productManager.Add(product);
                if(isSaved)
                {
                    return CustomResult("Producto creado", product, HttpStatusCode.Created);
                }
                return CustomResult("Hubo un error al crear el producto", product, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            try
            {
                if (string.IsNullOrEmpty(product.Id))
                    return CustomResult("No se encontro el producto", HttpStatusCode.NotFound);
               
                bool isUpdate = _productManager.Update(product.Id,product);
                if (isUpdate)
                {
                    return CustomResult("Producto actualizado", product, HttpStatusCode.OK);
                }
                return CustomResult("Hubo un error al actualizar el producto", product, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult DeleteProduct(string id  )
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return CustomResult("No se encontro el producto", HttpStatusCode.NotFound);

                bool isDelete = _productManager.Delete(id);
                if (isDelete)
                {
                    return CustomResult("Producto eliminado", HttpStatusCode.OK);
                }
                return CustomResult("Hubo un error al eliminar el producto", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);

            }
        }
    }
}
