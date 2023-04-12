using Microsoft.EntityFrameworkCore;

namespace WebApiProducts.Models
{
    public class ProductsContext: DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options)
           : base(options)
        {

        }
       
    }
}
