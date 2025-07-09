using System.ComponentModel.DataAnnotations;

namespace Polyempaques_API.Models
{
    public class MovimientosOdT1
    {
        [Key]
        public int idMovto { get; set; }
        public int idOdT { get; set; }
        public int idProducto { get; set; }
        public string serialNumber { get; set; }
        public int quantity { get; set; }
        public int idUsuario { get; set; }
        public DateTime? timestamp { get; set; }
    }
}
