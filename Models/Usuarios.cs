using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polyempaques_API.Models
{
    [Table("usuarios", Schema = "autenticacion")]
    public class Usuarios
    {
        [Key]
        public int idUsuario { get; set; }
        public string? nombreUsuario { get; set; }
        public string? usuario { get; set; }
        public string? contrasena { get; set; }
        public int idPerfil { get; set; }
        public DateTime fechayhora { get; set; }
        public string? email { get; set; }
    }
}
