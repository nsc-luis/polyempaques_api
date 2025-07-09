using System.ComponentModel.DataAnnotations;

namespace Polyempaques_API.Models
{
    public class Producto1
    {
        [Key]
        public int idProducto { get; set; }
        public string descripcion { get; set; }
        public string partNumber { get; set; }
        public int idUsuario { get; set; }
        public DateTime? timestamp { get; set; }
    }
}
