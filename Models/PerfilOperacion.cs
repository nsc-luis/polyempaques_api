using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polyempaques_API.Models
{
    [Table("PerfilOperacion", Schema = "autenticacion")]
    public class PerfilOperacion
    {
        [Key]
        public int idPerfilOperacion { get; set; }
        public int idPerfil { get; set; }
        public int idOperacion { get; set; }
    }
}
