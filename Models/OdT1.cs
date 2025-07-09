using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Polyempaques_API.Models
{
    public class OdT1
    {
        [Key]
        public int idOdT { get; set; }
        public int idProducto { get; set; }
        public string poNumber { get; set; }
        public int totalKgOT { get; set; }
        public int idUsuario { get; set; }
        public DateTime? timestamp { get; set; }
    }
}
