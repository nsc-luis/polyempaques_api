using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.IO.Pipelines;

namespace Polyempaques_API.Models
{
    public class QR
    {
        [Key]
        public int idQR { get; set; }
        public string descripcion { get; set; }
        public string partNumber { get; set; }
        public int quantity { get; set; }
        public string poNumber { get; set; }
        public string? trace { get; set; }
        public string serialNumber { get; set; }
        public DateTime? timestamp { get; set; }
        public int? idUsuario { get; set; }
        public int? ediciones { get; set; }
        public bool? activo { get; set; }
    }
}
