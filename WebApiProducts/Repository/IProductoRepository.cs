using Microsoft.AspNetCore.Mvc;
using WebApiProducts.Models;

namespace WebApiProducts.Repository
{
    public interface IProductoRepository
    {
        Task<List<Products>> GetProducts();
        Task<Products> GetProductById(int id);
        Task<bool> InsertProduct(Products Product);
        Task<bool> UpdateProduct(Products product);
        Task<bool> DeleteProduct(int id);
       
    }
}
