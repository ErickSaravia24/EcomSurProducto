using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProducts.Models
{
    [Table("BDProducts")]
    public class Products
    {
        public int productoId { get; set; }
        public string producto { get; set; }
        public float precio { get; set; }
        public int cantidadProducto { get; set; }
    }
}
