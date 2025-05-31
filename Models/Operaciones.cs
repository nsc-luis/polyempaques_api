using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polyempaques_API.Models
{
    [Table("operaciones", Schema = "autenticacion")]
    public class Operaciones
    {
        [Key]
        public int idOperacion { get; set; }
        public int idModulo { get; set; }
        public bool agregar { get; set; }
        public bool leer { get; set; }
        public bool editar { get; set; }
        public bool eliminar { get; set; }
    }
}
