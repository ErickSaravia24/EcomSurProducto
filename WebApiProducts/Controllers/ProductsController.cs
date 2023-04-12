using WebApiProducts.Models;
using Microsoft.AspNetCore.Mvc;
using WebApiProducts.Repository;
using System.Net;
using System;
using System.Drawing;

namespace WebApiProducts.Controllers
{
   
    [ApiController]
    [Route("/products")]
    public class ProductsController
    {
        private readonly string _connectionString;

        private readonly IProductoRepository _productRepository;

        public ProductsController(IProductoRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetProductsAll()
        {
            try
            {
                List<Products> products = await _productRepository.GetProducts();
                return new OkObjectResult(products);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { error = ex.Message });
            }
        }
       
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productRepository.GetProductById(id);
                return new OkObjectResult(product);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProduct(int id)
        {
            try
            {
                bool result = await _productRepository.DeleteProduct(id);
                if (result)
                {
                    return new OkObjectResult(result);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { error = ex.Message });
            }
        }
        [HttpPut]
        public async Task<ActionResult<bool>> Update(Products product)
        {
            try
            {
                bool result = await _productRepository.UpdateProduct(product);

                if (result)
                {
                    return new OkObjectResult(result);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { error = ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateProduct(Products product)
        {
            try
            {
                bool result = await _productRepository.InsertProduct(product);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { error = ex.Message });
            }
        }

    }
}
