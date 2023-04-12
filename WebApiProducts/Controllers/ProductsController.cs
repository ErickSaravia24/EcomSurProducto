using WebApiProducts.Models;
using Microsoft.AspNetCore.Mvc;
using WebApiProducts.Repository;

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
        public async Task<ActionResult<List<Products>>> GetProductsAll()
        {
            List<Products> products = await _productRepository.GetProducts();

            return products;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProductById(int id)
        {
            
                var product = await _productRepository.GetProductById(id);

              

                return product;

        }


        [HttpDelete("{id}")]
        public bool DeleteProduct(int id)
        {
            var result = _productRepository.DeleteProduct(id).GetAwaiter().GetResult();
            return result;
            
        }
        [HttpPut("{id}")]
        public bool Update(Products product)
        {
            var result = _productRepository.UpdateProduct(product).GetAwaiter().GetResult();
            return result;
        }
        [HttpPost]
        public bool CreateProduct(Products product)
        {

            var result = _productRepository.InsertProduct(product).GetAwaiter().GetResult();
            return result;
        }

    }
}
