using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polyempaques_API.Models
{
    [Table("perfiles", Schema = "autenticacion")]
    public class Perfiles
    {
        [Key]
        public int idPerfil { get; set; }
        public string? nombrePerfil { get; set; }
    }
}
