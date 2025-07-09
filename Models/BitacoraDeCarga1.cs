using System.ComponentModel.DataAnnotations;

namespace Polyempaques_API.Models
{
    public class BitacoraDeCarga1
    {
        [Key]
        public int idBitCarga { get; set; }
        public int idOdT { get; set; }
        public string mensaje { get; set; }
        public int idUsuario { get; set; }
        public DateTime? timestamp { get; set; }
    }
}
